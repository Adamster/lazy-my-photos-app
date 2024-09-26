using Android;
using Android.OS;
using AndroidX.Core.App;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;

namespace Lazy.MyPhotos.App.Platforms.Android.Handlers
{
    public class RequestPhotoAccessPermissionHandler : IRequestPhotoAccessPermissionHandler
    {
        const int RequestStorageId = 1000;

        public Task<bool> ExecuteAsync()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13+
            {
                ActivityCompat.RequestPermissions(Platform.CurrentActivity, new string[] {
                    Manifest.Permission.ReadMediaImages,
                    Manifest.Permission.ReadMediaVideo
                }, RequestStorageId);
            }


            ActivityCompat.RequestPermissions(Platform.CurrentActivity, new string[] { Manifest.Permission.ReadExternalStorage }, RequestStorageId);
            return Task.FromResult(true);
        }
    }
}