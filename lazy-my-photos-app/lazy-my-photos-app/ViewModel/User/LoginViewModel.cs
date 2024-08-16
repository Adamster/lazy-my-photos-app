using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Messages.User;
using Lazy.MyPhotos.App.Services;
using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.View.User;
using Lazy.MyPhotos.Shared.Models.User;


namespace Lazy.MyPhotos.App.ViewModel.User;

[ObservableObject]
public partial class LoginViewModel
{

    private CancellationTokenSource _cancellationTokenSource = new();
    private readonly IUserApi _userApi;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    public LoginViewModel(IUserApi userApi)
    {
        _userApi = userApi;
    }

    [RelayCommand]
    private async Task Login()
    {
        string message;
        var loginRequest = new LoginRequest(Email, Password);
        var loginResponse = await _userApi.Login(loginRequest);

        _cancellationTokenSource = new CancellationTokenSource();

        if (loginResponse.IsSuccessStatusCode)
        {
            message = $"Your login was successful {loginResponse.Content!.AccessToken}";
        }
        else
        {       
            message = $"Login failed {loginResponse.Error.Content}";
        }


        Application.Current?.MainPage?.DisplayAlert("Login", message, "ok");

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