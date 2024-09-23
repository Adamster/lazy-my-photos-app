namespace Lazy.MyPhotos.Shared.Permissions;

public interface IPhotoPermissionService
{
    Task<bool> CheckStatusAsync();

    Task<bool> RequestAsync();

}