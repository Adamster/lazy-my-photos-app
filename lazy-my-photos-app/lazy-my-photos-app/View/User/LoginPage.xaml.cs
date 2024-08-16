using Lazy.MyPhotos.App.ViewModel.User;

namespace Lazy.MyPhotos.App.View.User;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}