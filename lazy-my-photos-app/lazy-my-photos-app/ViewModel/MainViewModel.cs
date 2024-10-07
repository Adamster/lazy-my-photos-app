using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.View.User;


namespace Lazy.MyPhotos.App.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    private void Register()
    {
        var registerPage = Application.Current!.Handler!.MauiContext!.Services.GetService<RegisterPage>();
        Application.Current.MainPage = registerPage;
    }

    [RelayCommand]
    private void Login()
    {
        var loginPage = Application.Current!.Handler!.MauiContext!.Services.GetService<LoginPage>();
        Application.Current.MainPage = loginPage;
    }
}