using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Messages.User;
using Lazy.MyPhotos.App.View.User;
using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.ViewModel.User;

[ObservableObject]
public partial class LoginViewModel(IUserApi userApi)
{
    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [RelayCommand]
    private async Task Login()
    {
        var loginRequest = new LoginRequest(Email, Password);
        var loginResponse = await userApi.Login(loginRequest);


        if (loginResponse is {IsSuccessStatusCode: true, Content: not null})
        {
            WeakReferenceMessenger.Default.Send(new UserLoggedInMessage(loginResponse.Content));
        }
    }

    [RelayCommand]
    private async Task Register()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}