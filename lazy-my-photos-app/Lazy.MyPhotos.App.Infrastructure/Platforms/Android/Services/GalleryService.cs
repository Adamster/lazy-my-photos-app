using Android.Provider;
using Lazy.MyPhotos.Shared.Services.Interfaces;


namespace Lazy.MyPhotos.App.Infrastructure.Platforms.Android.Services;

public class GalleryService : IGalleryService
{
    public async Task<List<MemoryStream>> GetPhotoStreams(int currentPage, int pageSize)
    {
        var photos = new List<MemoryStream>();
       // return photos;
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
                            var stream = await BuildMemoryStream(imagePath);
                            photos.Add(stream);
                        }
                    }

                    count++;
                } while (cursor.MoveToNext());
            }
        }

        return photos;
    }

    private async Task<MemoryStream> BuildMemoryStream(string imagePath)
    {
        var bytes = await File.ReadAllBytesAsync(imagePath);

        return new MemoryStream(bytes);
    }
}