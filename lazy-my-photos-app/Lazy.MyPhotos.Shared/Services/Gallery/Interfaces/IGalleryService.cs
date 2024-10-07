namespace Lazy.MyPhotos.Shared.Services.Gallery.Interfaces;

public interface IGalleryService
{
    Task<List<Stream>> GetPhotoStreams(int currentPage, int pageSize, bool isThumbnail = true);
}