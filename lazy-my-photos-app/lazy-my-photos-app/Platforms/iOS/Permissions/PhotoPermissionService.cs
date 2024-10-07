using Lazy.MyPhotos.App.Platforms.iOS.Permissions;
using Lazy.MyPhotos.Shared.Permissions;
using Photos;

[assembly: Dependency(typeof(PhotoPermissionService))]

namespace Lazy.MyPhotos.App.Platforms.iOS.Permissions;

public class PhotoPermissionService : IPhotoPermissionService
{
    public async Task<bool> CheckStatusAsync()
    {
        var status = await PHPhotoLibrary.RequestAuthorizationAsync(PHAccessLevel.ReadWrite);
        return status == PHAuthorizationStatus.Authorized;
    }

    public async Task<bool> RequestAsync()
    {
        var result = await PHPhotoLibrary.RequestAuthorizationAsync(PHAccessLevel.ReadWrite);
        return result == PHAuthorizationStatus.Authorized;
    }
}