#if IOS
using CoreGraphics;
using Foundation;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Photos;
using UIKit;


namespace Lazy.MyPhotos.App.Modules.Photo.Services;

// ReSharper disable once InconsistentNaming
public class iOSGalleryService : IGalleryService
{
    private readonly ILogger<iOSGalleryService> _logger;

    public iOSGalleryService(ILogger<iOSGalleryService> logger)
    {
        _logger = logger;
    }

    public IList<string> GetPhotoPaths()
    {
        var photos = new List<string>();
        return photos;
    }

    public IList<Stream> GetPhotoStreams(int currentPage, int pageSize)
    {
        var skip = currentPage * pageSize;
        var take = pageSize;
        var fetchOptions = new PHFetchOptions() { };
        fetchOptions.SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", false) };


        // Fetch all image assets
        var fetchResult = PHAsset.FetchAssets(PHAssetMediaType.Image, fetchOptions);
        _logger.LogInformation("total photo count: {0}", fetchResult.Count);
        var count = fetchResult.Count;

        // Ensure that skip and take are within the bounds of the total count
        IntPtr startIndex = Math.Min(skip, count);
        IntPtr endIndex = Math.Min(startIndex + take, count);

        _logger.LogInformation("start index: {0}", startIndex);
        _logger.LogInformation("end index: {0}", endIndex);

        var imageList = new List<UIImage>();

        for (IntPtr i = startIndex; i < endIndex; i++)
        {
            var asset = fetchResult[i];
            var options = new PHImageRequestOptions
            {
                Synchronous = false, // Ensure images are loaded synchronously for this example
                ResizeMode = PHImageRequestOptionsResizeMode.Fast,
            };

            var imageAsset = asset as PHAsset;

            PHImageManager.DefaultManager.RequestImageForAsset(imageAsset, new CGSize(300, 300), PHImageContentMode.AspectFit, options, (image, info) =>
            {
                if (image != null)
                {
                    imageList.Add(image);
                }
            });
        }

        //fetchResult.Enumerate((NSObject asset, UIntPtr index, out bool stop) =>
        //{
        //    // Request image for each asset
        //    var options = new PHImageRequestOptions
        //    {
        //        Synchronous = true,  // Get image synchronously
        //        ResizeMode = PHImageRequestOptionsResizeMode.Fast
        //    };

        //    var imageAsset = asset as PHAsset;
        //    PHImageManager.DefaultManager.RequestImageForAsset(imageAsset!, new CGSize(100, 100), PHImageContentMode.AspectFit, options, (image, info) =>
        //    {
        //        if (image != null)
        //        {
        //            imageList.Add(image);
        //            _logger.LogInformation("Photo added to image list");
        //        }
        //    });

        //    previousCount++;
        //    _logger.LogInformation("Image count: {0}", previousCount);
        //    stop = previousCount > 100; // > imageList.Count;
        //});


        var streams = imageList
            .Select(x => x.AsPNG()!.AsStream()).ToList();

      
        return streams;
    }
}
#endif