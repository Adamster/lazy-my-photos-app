
using Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

namespace Lazy.MyPhotos.App.Modules.Photo.Mvvm.Views;

public partial class PhotoGalleryView : ContentView
{
    private readonly PhotoGalleryViewModel _vm;

    public PhotoGalleryView()
	{
		InitializeComponent();
       _vm = App.Current.Handler.MauiContext.Services.GetService<PhotoGalleryViewModel>()!;
       BindingContext = _vm;
    }

    private async void GalleryCollectionView_OnRemainingItemsThresholdReached(object? sender, EventArgs e)
    {
        var vm = BindingContext as PhotoGalleryViewModel;
        await vm!.LoadPhotos();
    }
}