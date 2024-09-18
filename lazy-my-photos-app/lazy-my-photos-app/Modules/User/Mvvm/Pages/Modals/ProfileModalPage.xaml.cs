using Lazy.MyPhotos.App.Modules.User.Mvvm.ViewModel.Modals;

namespace Lazy.MyPhotos.App.Modules.User.Mvvm.Pages.Modals;

public partial class ProfileModalPage : ContentPage
{
	public ProfileModalPage(ProfileModalViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}