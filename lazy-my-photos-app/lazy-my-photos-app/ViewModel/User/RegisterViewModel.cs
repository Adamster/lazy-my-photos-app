using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.View.User;
using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.ViewModel.User;

[ObservableObject]
public partial class RegisterViewModel
{

    private CancellationTokenSource _cancellationTokenSource = new();
    private readonly IUserApi _userApi;

    public RegisterViewModel(IUserApi userApi)
    {
        _userApi = userApi;
    }

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;



    [RelayCommand]
    private async Task Login()
    {
        var loginPage = App.Current.Handler.MauiContext.Services.GetService<LoginPage>();
        App.Current.MainPage = loginPage;
    }

    [RelayCommand]
    private async Task Register()
    {
        var registerRequest = new RegisterRequest(Email, Password);
        var result = await _userApi.Register(registerRequest);


        _cancellationTokenSource = new CancellationTokenSource();
        string message;
        if (result.IsSuccessStatusCode)
        {
            message = $"Your account was created {result}";

        }
        else
        {
            message = $"Registration failed: {result.Error.Content}";
        }
        Application.Current?.MainPage?.DisplayAlert("Register", message, "ok");
    }
}