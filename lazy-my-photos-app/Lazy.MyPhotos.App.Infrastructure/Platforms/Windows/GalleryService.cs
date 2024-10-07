using Lazy.MyPhotos.Shared.Services.Gallery.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Infrastructure.Platforms.Windows;

public class GalleryService(ILogger<GalleryService> logger) : IGalleryService
{
    public Task<List<Stream>> GetPhotoStreams(int currentPage, int pageSize, bool isThumbnail = true)
    {
        logger.LogInformation("Get photo called on Windows");
        var streams = new List<Stream>();

        return Task.FromResult(streams);
    }
}
