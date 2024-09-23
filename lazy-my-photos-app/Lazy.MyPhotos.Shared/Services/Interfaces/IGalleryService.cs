namespace Lazy.MyPhotos.Shared.Services.Interfaces;

public interface IGalleryService
{
    IList<string> GetPhotoPaths();
}