using Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.Shared.Services.Interfaces;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Impl
{
    internal sealed class PhotoScanService : IPhotoScanService
    {
        private readonly IGalleryService _galleryService;
        private readonly ICheckSumProvider _checkSumProvider;
      
        //public PhotoScanService(IGalleryService galleryService,
        //    ICheckSumProvider checkSumProvider)
        //{
        //    _galleryService = galleryService;
        //    _checkSumProvider = checkSumProvider;
        //}

        public void StartScan()
        {
            Task.Run(StartScanInternal);
        }

        private async Task StartScanInternal()
        {
            var currentPage = 0;
            List<MemoryStream> photoStreams = await _galleryService.GetPhotoStreams(currentPage, 10);

            do
            {
                foreach (var photoStream in photoStreams)
                {
                    //calculate checkSum
                    byte[] checkSum = _checkSumProvider.CalculateChecksum(photoStream.ToArray());
                    //check if photo is present
                    //save if not
                    //publish metadata extractor message with photo Id

                }

                currentPage++;
            }
            while (photoStreams.Any());
        }

        public void StopScan()
        {
            throw new NotImplementedException();
        }
    }
}
