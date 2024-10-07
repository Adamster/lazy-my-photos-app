
using CoreGraphics;
using Foundation;
using Lazy.MyPhotos.Shared.Services.Gallery.Interfaces;
using Microsoft.Extensions.Logging;
using Photos;
using UIKit;

namespace Lazy.MyPhotos.App.Infrastructure.Platforms.MacCatalyst.Services;

public class GalleryService(ILogger<GalleryService> logger) : IGalleryService
{
    public Task<List<Stream>> GetPhotoStreams(int currentPage, int pageSize, bool isThumbnail = true)
    {
        logger.LogInformation("Get photo called on mac");
        var streams = new List<Stream>();

        return Task.FromResult(streams);
    }
}
