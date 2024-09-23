using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;

namespace Lazy.MyPhotos.App.Modules.Photo.Handlers.Impl;

public class RequestPhotoAccessPermissionHandler : IRequestPhotoAccessPermissionHandler
{
    public async Task<bool> ExecuteAsync()
    {

        PermissionStatus status;

        if (OperatingSystem.IsAndroidVersionAtLeast(33))
        {
            status = await Permissions.RequestAsync<Permissions.Media>();
        }
        else
        {
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        }

        if (status != PermissionStatus.Granted)
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(33))
            {
                status = await Permissions.RequestAsync<Permissions.Media>();
            }
            else
            {
                status = await Permissions.RequestAsync<Permissions.Media>();
            }
        }

        return status == PermissionStatus.Granted;
    }
}