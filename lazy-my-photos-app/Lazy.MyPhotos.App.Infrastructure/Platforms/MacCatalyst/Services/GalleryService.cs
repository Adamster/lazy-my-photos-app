
using CoreGraphics;
using Foundation;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Photos;
using UIKit;

namespace Lazy.MyPhotos.App.Infrastructure.Platforms.MacCatalyst.Services;

public class GalleryService(ILogger<GalleryService> logger) : IGalleryService
{
    public Task<List<MemoryStream>> GetPhotoStreams(int currentPage, int pageSize, bool isThumbnail = true)
    {
        var streams = new List<MemoryStream>();

        return Task.FromResult(streams);
    }
}
