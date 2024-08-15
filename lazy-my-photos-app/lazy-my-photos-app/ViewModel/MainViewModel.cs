using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace lazy_my_photos_app.ViewModel;

[ObservableObject]
public partial class MainViewModel
{
    [RelayCommand]
    async Task Register()
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }

    [RelayCommand]
    async Task Login()
    {
        await Shell.Current.GoToAsync("LoginPage");
    }
}