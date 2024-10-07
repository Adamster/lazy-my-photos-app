
using Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

namespace Lazy.MyPhotos.App.Modules.Photo.Mvvm.Views;

public partial class PhotoGalleryView : ContentView
{
    public PhotoGalleryView()
	{
        InitializeComponent();
       PhotoGalleryViewModel vm = Application.Current!.Handler!.MauiContext!.Services.GetService<PhotoGalleryViewModel>()!;
       BindingContext = vm;
    }
}