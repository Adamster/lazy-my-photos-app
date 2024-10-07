﻿using Android.Graphics;
using Android.OS;
using Android.Provider;
using Lazy.MyPhotos.Shared.Services.Gallery.Interfaces;
using Debug = System.Diagnostics.Debug;

namespace Lazy.MyPhotos.App.Infrastructure.Platforms.Android.Services;

public class GalleryService : IGalleryService
{
    public async Task<List<Stream>> GetPhotoStreams(int currentPage, int pageSize, bool isThumbnail = true)
    {
        var photos = new List<Stream>();
        var uri = MediaStore.Images.Media.ExternalContentUri;

        string[] projection =
        [
            MediaStore.IMediaColumns.Data
        ];

        string sortOrder = $"{MediaStore.Images.IImageColumns.DateTaken} DESC";

        using var cursor = Platform.CurrentActivity.ContentResolver.Query(uri, projection, null, null, sortOrder);
        if (cursor != null)
        {
            int totalCount = cursor.Count;

            if (totalCount == 0 || currentPage * pageSize >= totalCount)
            {
                // No data or invalid page request
                return photos;
            }

            // Calculate the start position
            int startPosition = currentPage * pageSize;

            if (cursor.MoveToPosition(startPosition))
            {
                
                int count = 0;

                int columnIndex = cursor.GetColumnIndex(MediaStore.Images.Thumbnails.Data);

                do
                {
                    if (count >= pageSize)
                    {
                        break;
                    }

                    var imagePath = cursor.GetString(columnIndex);
                    if (imagePath != null)
                    {
                        var fileExists = File.Exists(imagePath);
                        if (fileExists)
                        {
                            var stream = await BuildMemoryStream(imagePath, isThumbnail);
                            photos.Add(stream);
                        }
                    }

                    count++;
                } while (cursor.MoveToNext());
            }
        }

        return photos;
    }

    private async Task<Stream> BuildMemoryStream(string imagePath, bool isThumbnail)
    {
        var bytes = await File.ReadAllBytesAsync(imagePath);
        if (isThumbnail)
        { 
            var memoryStream = new MemoryStream(bytes);
            var scaledStream = ScaleDownImageAndroid(memoryStream, 100, 100);
            return scaledStream;
        }

        return new MemoryStream(bytes);
    }

    private Stream ScaleDownImageAndroid(Stream imageStream, int maxWidth, int maxHeight)
    {
        // Decode the image bounds without loading it into memory
        BitmapFactory.Options options = new()
        {
            InJustDecodeBounds = true
        };

        // Calculate the scaling factor
        int scaleFactor = Math.Min(options.OutWidth / maxWidth, options.OutHeight / maxHeight);

        // Decode the image at the new size
        options.InJustDecodeBounds = false;
        options.InSampleSize = scaleFactor;

        // Reset the stream position
        imageStream.Position = 0;

        Bitmap bitmap = BitmapFactory.DecodeStream(imageStream, null, options)!;

        // Compress the scaled bitmap and return it as a Stream
        MemoryStream outputStream = new MemoryStream();
        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 80, outputStream);
        outputStream.Position = 0;
        bitmap.Recycle();
        return outputStream;
    }
}