using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;
using Lazy.MyPhotos.App.Modules.Photo.Mediators.Interfaces;
using Lazy.MyPhotos.App.Modules.Photo.Models;
using Lazy.MyPhotos.Persistence.Entities;
using Lazy.MyPhotos.Shared.Services.Photo.Sync;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lazy.MyPhotos.App.Modules.Photo.Services.Impl
{
    internal sealed class PhotoSyncService : IPhotoSyncService
    {
        private readonly ILogger<PhotoSyncService> _logger;
        private readonly IGetNewPhotosHandler _getNewPhotosHandler;
        private readonly IPhotoHashMediator _photoHashMediator;
        private readonly ISaveMultiplePhotosHandler _saveMultiplePhotosHandler;
        private readonly IUploadPhotosHandler _uploadPhotosHandler;
        private readonly IVerifyUploadHashResultHandler _verifyUploadHashResultHandler;
        private readonly IMarkPhotosSyncedHandler _markPhotosSyncedHandler;
        private CancellationTokenSource _cts = new();
        private Stopwatch _sw = new();


        public PhotoSyncService(ILogger<PhotoSyncService> logger,
            IGetNewPhotosHandler getNewPhotosHandler,
            IPhotoHashMediator photoHashMediator,
            ISaveMultiplePhotosHandler saveMultiplePhotosHandler,
            IUploadPhotosHandler uploadPhotosHandler,
            IVerifyUploadHashResultHandler verifyUploadHashResultHandler,
            IMarkPhotosSyncedHandler markPhotosSyncedHandler)
        {
            _logger = logger;
            _getNewPhotosHandler = getNewPhotosHandler;
            _photoHashMediator = photoHashMediator;
            _saveMultiplePhotosHandler = saveMultiplePhotosHandler;
            _uploadPhotosHandler = uploadPhotosHandler;
            _verifyUploadHashResultHandler = verifyUploadHashResultHandler;
            _markPhotosSyncedHandler = markPhotosSyncedHandler;
        }

        public async Task StartSync()
        {
            _cts = new CancellationTokenSource();

            _logger.LogInformation("Check for new photos on devices");

            IList<PhotoItem> newPhotos = await _getNewPhotosHandler.ExecuteAsync(_cts.Token);
            _logger.LogInformation("New {0} photos found", newPhotos.Count);


            _logger.LogInformation("Generating hash for every photo");

            _sw.Start();
            IList<LazyPhoto> photosWithHash = await _photoHashMediator.ExecuteAsync(newPhotos, _cts.Token);
            _sw.Stop();
            _logger.LogInformation("Hash generated for {0} photos. It took {1}ms", photosWithHash.Count, _sw.Elapsed.TotalMilliseconds);

            // Save to local db 
            bool insertResult = await _saveMultiplePhotosHandler.ExecuteAsync(photosWithHash, _cts.Token);


            if (!insertResult)
            {
                _logger.LogError("Photo inserting failed!");
               return;
            }

            // Upload file one by one
            IList<LazyPhoto> uploadResult = await _uploadPhotosHandler.ExecuteAsync(photosWithHash, _cts.Token);

            // Compare hash 
            var uploadIsSuccessful = await _verifyUploadHashResultHandler.ExecuteAsync(uploadResult, _cts.Token);

            if (!uploadIsSuccessful)
            {
                _logger.LogError("Photo upload failed");
                return;
            }

            // If hash matched update sync state 
            await _markPhotosSyncedHandler.ExecuteAsync(uploadResult, _cts.Token);
        }

        public async Task StopSync()
        {
            _logger.LogInformation("Sync cancel requested");

            await _cts.CancelAsync();

            _logger.LogInformation("Sync stopped");
        }
    }
}
