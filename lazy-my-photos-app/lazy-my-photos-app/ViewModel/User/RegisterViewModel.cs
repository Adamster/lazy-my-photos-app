using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.View.User;

namespace Lazy.MyPhotos.App.ViewModel.User;

[ObservableObject]
public partial class RegisterViewModel
{
    [RelayCommand]
    private async Task Login()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}