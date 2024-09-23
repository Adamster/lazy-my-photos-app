namespace Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;

public interface IRequestPhotoAccessPermissionHandler
{
    Task<bool> ExecuteAsync();
}