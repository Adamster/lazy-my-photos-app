using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Modules.Photo.Models;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

[ObservableObject]
public partial class PhotoGalleryViewModel
{
    private readonly IPhotoApi _photoApi;
    private readonly IPhotoContentApi _photoContentApi;
    private readonly ILogger<PhotoGalleryViewModel> _logger;

    [ObservableProperty]
    ObservableCollection<PhotoItem> _photos = new();


    public PhotoGalleryViewModel(IPhotoApi photoApi, IPhotoContentApi photoContentApi, ILogger<PhotoGalleryViewModel> logger)
    {
        _photoApi = photoApi;
        _photoContentApi = photoContentApi;
        _logger = logger;
    }

    public async Task LoadPhotos()
    {
        var photos = await _photoApi.GetPhotos();

        if (photos.IsSuccessStatusCode)
        {
            var photoModels = photos.Content!.Select(x => new PhotoItem(x.Id, x.DisplayFileName) { Image = ImageSource.FromFile("sloth.png") });

            foreach (var photoModel in photoModels)
            {
                _photos.Add(photoModel);
            }

            var options = new ParallelOptions { MaxDegreeOfParallelism = 5 };
            await Parallel.ForEachAsync(Photos, options, async (p, token) =>
             {
                 var imageContent = await BuildImageSource(p.Id);
                 p.Image = imageContent ?? ImageSource.FromFile("dotnet_bot.png");
             });


        }
    }

    private async Task<ImageSource?> BuildImageSource(long photoId)
    {
        _logger.LogInformation("Downloading photo content for id {0}", photoId);
        var imageStreamResponse = await _photoContentApi.GetPhoto(photoId);
        if (imageStreamResponse.IsSuccessStatusCode)
        {
            var stream = new MemoryStream();
            await imageStreamResponse.Content!.CopyToAsync(stream);


            var bytes = stream.ToArray();
            return ImageSource.FromStream(() =>
            {
                _logger.LogInformation("Creating image source");
                return new MemoryStream(bytes);
            });
        }

        return null;
    }
}