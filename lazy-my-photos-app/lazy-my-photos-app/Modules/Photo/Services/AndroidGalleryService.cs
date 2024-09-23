
using Android.Provider;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Lazy.MyPhotos.App.Modules.Photo.Services;

public class AndroidGalleryService : IGalleryService
{
    public IList<string> GetPhotoPaths()
    {
        var photos = new List<string>();
#if ANDROID


        var uri = MediaStore.Images.Media.ExternalContentUri;

        string[] projection = [MediaStore.Images.Media.InterfaceConsts.Data];

        var context = Android.App.Application.Context;

        var cursor = context.ContentResolver?.Query(uri, projection, null, null, null);

        if (cursor != null && cursor.MoveToFirst())
        {
            int columnIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);

            do
            {
                string? path = cursor.GetString(columnIndex);
                if (path is not null)
                {
                    photos.Add(path);
                }
            } while (cursor.MoveToNext());
        }

        cursor?.Close();
#endif
        return photos;
    }
}