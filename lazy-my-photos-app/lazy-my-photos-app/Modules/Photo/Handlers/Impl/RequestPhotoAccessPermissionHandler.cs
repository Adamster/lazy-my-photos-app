using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;

namespace Lazy.MyPhotos.App.Modules.Photo.Handlers.Impl
{
    public class RequestPhotoAccessPermissionHandler : IRequestPhotoAccessPermissionHandler
    {
        public async Task<bool> ExecuteAsync()
        {
            PermissionStatus status = await Permissions.RequestAsync<Permissions.Media>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Media>();
            }

            return status == PermissionStatus.Granted;
        }
    }
}