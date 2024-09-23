using Lazy.MyPhotos.App.Platforms.Android.Permissions;
using Lazy.MyPhotos.Shared.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using static AndroidX.Activity.Result.Contract.ActivityResultContracts;

[assembly: Dependency(typeof(PhotoPermissionService))]

namespace Lazy.MyPhotos.App.Platforms.Android.Permissions
{
    internal class PhotoPermissionService : IPhotoPermissionService
    {
        public Task<bool> CheckStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestAsync()
        {
            throw new NotImplementedException();
        }
    }
}
