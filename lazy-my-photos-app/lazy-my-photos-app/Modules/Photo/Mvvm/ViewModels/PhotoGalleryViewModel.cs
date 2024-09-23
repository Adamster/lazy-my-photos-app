using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;
using Lazy.MyPhotos.App.Modules.Photo.Models;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

[ObservableObject]
public partial class PhotoGalleryViewModel
{
    private const int PageSize = 25;
    private int _currentPage = 0;


    private readonly IPhotoApi _photoApi;
    private readonly IPhotoContentApi _photoContentApi;
    private readonly IGalleryService _galleryService;
    private readonly IRequestPhotoAccessPermissionHandler _photoAccessPermissionHandler;
    private readonly ILogger<PhotoGalleryViewModel> _logger;

    [ObservableProperty]
    ObservableCollection<PhotoItem> _photos = new();

    private bool _isLoading;


    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public PhotoGalleryViewModel(
        IPhotoApi photoApi,
        IPhotoContentApi photoContentApi,
        IGalleryService galleryService,
        IRequestPhotoAccessPermissionHandler photoAccessPermissionHandler,
        ILogger<PhotoGalleryViewModel> logger)
    {
        _photoApi = photoApi;
        _photoContentApi = photoContentApi;
         _galleryService = galleryService;
        _photoAccessPermissionHandler = photoAccessPermissionHandler;
        _logger = logger;
    }

    [RelayCommand]
    public async Task LoadPhotos()
    {
        _logger.LogInformation("Load photos called");
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;

       

        var newPhotos = GetPhotosPage(_currentPage, PageSize);

        
        foreach (var photo in newPhotos)
        {
            Photos.Add(photo);
        }

    
        _currentPage++;

        IsLoading = false;

      
    }

    public async Task EnsurePermissionAccess()
    {
        var photoAccessGranted = await _photoAccessPermissionHandler.ExecuteAsync();

        if (!photoAccessGranted)
        {
            await Application.Current.MainPage.DisplayAlert("Permission Required", "App needs access to photos to proceed.", "OK");
            return;
        }
    }

    private  IEnumerable<PhotoItem> GetPhotosPage(int currentPage, int pageSize)
    {
        _logger.LogInformation("Loading new page {0}", currentPage);
        var photoPage = new List<PhotoItem>();
        var photoPaths = _galleryService.GetPhotoPaths().Skip(currentPage*pageSize).Take(pageSize);

        foreach (var photoPath in photoPaths)
        {
            var photoId = photoPath.GetHashCode();
            var photoName = Path.GetFileName(photoPath);

            var photoItem = new PhotoItem(photoId, photoName, PhotoItemType.Local)
            {
                Image = ImageSource.FromFile(photoPath)
            };

            photoPage.Add(photoItem);
        }

        return photoPage;

        //var photos = await _photoApi.GetPhotos();

        //if (photos is { IsSuccessStatusCode: true, Content: not null })
        //{
        //    var photoModels = BuildPhotoItems(photos.Content);
        //    PopulatePhotoCollection(photoModels);
        //    await DownloadPhotoContent();
        //}
    }

    private IEnumerable<PhotoItem> BuildPhotoItems(PhotoItemModel[] photoItemModels)
    {
        return photoItemModels.Select(x =>
            new PhotoItem(x.Id, x.DisplayFileName, PhotoItemType.Cloud)
            {
                Image = ImageSource.FromFile("sloth.png")
            });
    }

    private void PopulatePhotoCollection(IEnumerable<PhotoItem> photoModels)
    {
        foreach (var photoModel in photoModels)
        {
            _photos.Add(photoModel);
        }
    }

    private async Task DownloadPhotoContent()
    {
        var options = new ParallelOptions { MaxDegreeOfParallelism = 5 };
        var cloudPhotos = Photos.Where(x => x.PhotoItemType == PhotoItemType.Cloud);
        await Parallel.ForEachAsync(cloudPhotos, options, async (p, token) =>
        {
            var imageContent = await BuildImageSource(p.Id);
            p.Image = imageContent ?? ImageSource.FromFile("dotnet_bot.png");
        });
    }

    private async Task<ImageSource?> BuildImageSource(long photoId)
    {
        _logger.LogInformation("Downloading photo content for id {0}", photoId);
        var imageStreamResponse = await _photoContentApi.GetPhoto(photoId);

        if (!imageStreamResponse.IsSuccessStatusCode)
        {
            _logger.LogWarning("Photo content not available for id: {0}", photoId);
            return null;
        }
        var stream = new MemoryStream();
        await imageStreamResponse.Content!.CopyToAsync(stream);

        var bytes = stream.ToArray();
        return ImageSource.FromStream(() =>
        {
            _logger.LogInformation("Creating image source");
            return new MemoryStream(bytes);
        });

    }

    public async Task LoadFirstPage()
    {
        await LoadPhotos();
    }
}