using Lazy.MyPhotos.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.MyPhotos.App.Infrastructure.Platforms.Windows
{
    public class GalleryService : IGalleryService
    {
        public Task<List<MemoryStream>> GetPhotoStreams(int currentPage, int pageSize, bool isThumbnail = true)
        {
            throw new NotImplementedException();
        }
    }
}
