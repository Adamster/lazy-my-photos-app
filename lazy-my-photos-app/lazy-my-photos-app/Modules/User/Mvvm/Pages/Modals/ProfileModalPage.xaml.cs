using Lazy.MyPhotos.App.Modules.User.Mvvm.ViewModel.Modals;
using Microsoft.Maui.Controls;

namespace Lazy.MyPhotos.App.Modules.User.Mvvm.Pages.Modals;

public partial class ProfileModalPage : ContentPage
{
	public ProfileModalPage(ProfileModalViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}