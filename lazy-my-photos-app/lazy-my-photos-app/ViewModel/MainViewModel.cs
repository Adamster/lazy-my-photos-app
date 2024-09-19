using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.View.User;


namespace Lazy.MyPhotos.App.ViewModel;

[ObservableObject]
public partial class MainViewModel
{
    [RelayCommand]
    private async Task Register()
    {
        var registerPage = Application.Current.Handler.MauiContext.Services.GetService<RegisterPage>();
        App.Current.MainPage = registerPage;
    }

    [RelayCommand]
    private async Task Login()
    {
        var loginPage = App.Current.Handler.MauiContext.Services.GetService<LoginPage>();
        App.Current.MainPage = loginPage;
    }
}