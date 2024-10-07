namespace Lazy.MyPhotos.Shared.Services.Photo.Sync
{
    public interface IPhotoSyncService
    {
        Task StartSync();

        Task StopSync();
    }
}