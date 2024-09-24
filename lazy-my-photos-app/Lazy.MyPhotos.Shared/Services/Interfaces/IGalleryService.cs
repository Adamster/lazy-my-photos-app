namespace Lazy.MyPhotos.Shared.Services.Interfaces;

public interface IGalleryService
{
    IList<string> GetPhotoPaths();

    IList<Stream> GetPhotoStreams(int currentPage, int pageSize);
  
}