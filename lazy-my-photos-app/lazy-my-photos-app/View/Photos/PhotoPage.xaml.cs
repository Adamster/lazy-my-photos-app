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
        var vm = GalleryView.BindingContext as PhotoGalleryViewModel;
        await vm.EnsurePermissionAccess();
        await vm.LoadFirstPage();
    }
}