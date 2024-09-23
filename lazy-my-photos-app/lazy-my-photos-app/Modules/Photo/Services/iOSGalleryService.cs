using Lazy.MyPhotos.Shared.Services.Interfaces;


namespace Lazy.MyPhotos.App.Modules.Photo.Services;

// ReSharper disable once InconsistentNaming
public class iOSGalleryService : IGalleryService
{
    public IList<string> GetPhotoPaths()
    {
        var photos = new List<string>();

        //var fetchOptions = new PHFetchOptions();
        //var result = PHAsset.FetchAssets(PHAssetMediaType.Image, fetchOptions);

        //result.Enumerate((asset, index, stop) =>
        //{
        //    var resources = PHAssetResource.GetAssetResources(asset);
        //    foreach (var resource in resources)
        //    {
        //        photos.Add(resource.OriginalFilename);
        //    }
        //});

        return photos;
    }
}