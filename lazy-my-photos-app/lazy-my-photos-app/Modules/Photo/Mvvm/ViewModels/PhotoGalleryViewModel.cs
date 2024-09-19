using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Modules.Photo.Models;

namespace Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

[ObservableObject]
public partial class PhotoGalleryViewModel
{
    private readonly IPhotoApi _photoApi;
    private readonly IPhotoContentApi _photoContentApi;

    [ObservableProperty]
    ObservableCollection<PhotoItem> _photos = new();


    public PhotoGalleryViewModel(IPhotoApi photoApi, IPhotoContentApi photoContentApi)
    {
        _photoApi = photoApi;
        _photoContentApi = photoContentApi;
    }

    public async Task LoadPhotos()
    {
        var photos = await _photoApi.GetPhotos();

        if (photos.IsSuccessStatusCode)
        {
            var content = photos.Content! as IEnumerable<PhotoItemModel>;
            var photoModels = content
                .Select(x =>
                    new PhotoItem(x.Id, x.DisplayFileName));

            foreach (var photoModel in photoModels)
            {
                var imageContent = await BuildImageSource(photoModel.Id);
                var photo = photoModel with { Image = imageContent };
                _photos.Add(photo);
            }
        }
    }

    private async Task<ImageSource> BuildImageSource(long photoId)
    {
        var imageStreamResponse = await _photoContentApi.GetPhoto(photoId);
        if (imageStreamResponse.IsSuccessStatusCode)
        {
            var stream = new MemoryStream();
            await imageStreamResponse.Content!.CopyToAsync(stream);

            var imageSource = ImageSource.FromStream(() => stream);
            return imageSource;
        }
        return ImageSource.FromFile("dotnet_bot.png");
    }
}