
using CoreGraphics;
using Foundation;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Photos;
using UIKit;

namespace Lazy.MyPhotos.App.Infrastructure.Platforms.iOS.Services;

public class GalleryService(ILogger<GalleryService> logger) : IGalleryService
{
    public List<string> GetPhotoPaths()
    {
        throw new NotImplementedException();
    }

    public Task<List<MemoryStream>> GetPhotoStreams(int currentPage, int pageSize)
    {
        int skip = currentPage * pageSize;
        int take = pageSize;
        var fetchOptions = new PHFetchOptions();
        fetchOptions.SortDescriptors = [new NSSortDescriptor("creationDate", false)];


        // Fetch all image assets
        var fetchResult = PHAsset.FetchAssets(PHAssetMediaType.Image, fetchOptions);
        var count = fetchResult.Count;

        logger.LogInformation("total photo count: {0}", count);

        // Ensure that skip and take are within the bounds of the total count
        nint startIndex = Math.Min(skip, count);
        nint endIndex = Math.Min(startIndex + take, count);

        var imageList = new List<UIImage>();

        for (nint i = startIndex; i < endIndex; i++)
        {
            NSObject asset = fetchResult[i];
            var options = new PHImageRequestOptions
            {
                Synchronous = false, // Ensure images are loaded synchronously for this example
                ResizeMode = PHImageRequestOptionsResizeMode.Exact,
                DeliveryMode = PHImageRequestOptionsDeliveryMode.HighQualityFormat
            };

            var imageAsset = asset as PHAsset;
            

            PHImageManager.DefaultManager.RequestImageForAsset(imageAsset!, new CGSize(imageAsset!.PixelWidth, imageAsset.PixelHeight), PHImageContentMode.Default, options, (image, info) =>
            {
                if (image != null)
                {
                    imageList.Add(image);
                }
            });
        }

        var photoStreams = new List<MemoryStream>();

        foreach (UIImage uiImage in imageList)
        {
            var memoryStream = new MemoryStream(uiImage.AsJPEG()!.ToArray());
            photoStreams.Add(memoryStream);
        }

        return Task.FromResult(photoStreams);
    }
}
