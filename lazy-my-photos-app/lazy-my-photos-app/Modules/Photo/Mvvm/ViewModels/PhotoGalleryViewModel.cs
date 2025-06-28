using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.ApiServices.Models.Photo;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;
using Lazy.MyPhotos.App.Modules.Photo.Models;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

public partial class PhotoGalleryViewModel : ObservableObject
{
    private const int PageSize = 50;
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

    public async Task LoadFirstPage()
    {
        await LoadPhotos();
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

        var newPhotos = await GetPhotosPage(_currentPage, PageSize);

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

    private async Task<IEnumerable<PhotoItem>> GetPhotosPage(int currentPage, int pageSize)
    {
        _logger.LogInformation("Loading new page {0}", currentPage);
        var photoList = new List<PhotoItem>();

        await GetPagedImages(currentPage, pageSize, photoList);

        return photoList;
    }

    private async Task GetPagedImages(int currentPage, int pageSize, List<PhotoItem> photoList)
    {
        var photoStreams = await _galleryService.GetPhotoStreams(currentPage, pageSize);

        foreach (var photoStream in photoStreams)
        {
            var photoId = 0;
            var photoName = Path.GetRandomFileName();
            var photoItem = new PhotoItem(photoId, photoName, PhotoItemType.Local)
            {
                Image = ImageSource.FromStream(() => new MemoryStream(photoStream.ToArray()))
            };

            photoList.Add(photoItem);

        }
    }


}