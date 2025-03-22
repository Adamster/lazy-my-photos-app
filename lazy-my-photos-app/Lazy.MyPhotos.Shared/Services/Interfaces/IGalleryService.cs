namespace Lazy.MyPhotos.Shared.Services.Interfaces;

public interface IGalleryService
{
    Task<List<MemoryStream>> GetPhotoStreams(int currentPage, int pageSize);
}