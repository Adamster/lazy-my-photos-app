using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.View.User;


namespace Lazy.MyPhotos.App.ViewModel.User;

[ObservableObject]
public partial class LoginViewModel
{
    [RelayCommand]
    private async Task Register()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}