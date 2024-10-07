using Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;

namespace Lazy.MyPhotos.App.View.Photos;

public partial class PhotoPage : ContentPage
{
	public PhotoPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (GalleryView.BindingContext is PhotoGalleryViewModel vm)
        {
            await vm.EnsurePermissionAccess();
            await vm.LoadFirstPage();
        }
    }
}