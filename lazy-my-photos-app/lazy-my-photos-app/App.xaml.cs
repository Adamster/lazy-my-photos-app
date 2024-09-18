using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Infrastructure.Services;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.App.Messages.User;
using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.ViewModel;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;
    private readonly IUserService _userService;
    private readonly ILogger<AppShellViewModel> _logger;


    public App(ISettingsService settingsService, IUserService userService, ILogger<AppShellViewModel> logger)
    {
        _settingsService = settingsService;
        _userService = userService;
        _logger = logger;

        InitializeComponent();

        WeakReferenceMessenger.Default.Register<UserLoggedInMessage>(this, UserLoggedInHandler);
        WeakReferenceMessenger.Default.Register<UserLogoutMessage>(this, UserLogoutHandler);

        if (string.IsNullOrEmpty(settingsService.AuthAccessToken))
        {
            MainPage = new MainPage();
            return;
        }

        PushNewAppShell();
    }

    private void UserLogoutHandler(object recipient, UserLogoutMessage message)
    {
        _settingsService.ClearAll();
        MainPage = new MainPage();
    }

    private void UserLoggedInHandler(object recipient, UserLoggedInMessage message)
    {
        _settingsService.AuthAccessToken = message.Value.AccessToken;
        PushNewAppShell();
    }

    private void PushNewAppShell()
    {
        var vm = new AppShellViewModel(_userService, _logger);
        MainPage = new AppShell(vm);
    }
}