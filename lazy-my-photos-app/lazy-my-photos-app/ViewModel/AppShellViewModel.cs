using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Infrastructure.Messages.User;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.App.Modules.User.Mvvm.Pages.Modals;
using Lazy.MyPhotos.App.Modules.User.Mvvm.ViewModel.Modals;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.ViewModel;

public partial class AppShellViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly ILogger<AppShellViewModel> _logger;

    [ObservableProperty]
    private string _userName = string.Empty;

    public AppShellViewModel(IUserService userService, ILogger<AppShellViewModel> logger)
    {
        _userService = userService;
        _logger = logger;

        Task.Run(LoadUserName);
    }

    public async Task LoadUserName()
    {
        var user = await _userService.GetUser();
        if (user == null)
        {
            _logger.LogWarning("User is null");
            LogoutInternal();
            return;
        }


        UserName = user.UserName.Split('@')[0];
    }

    [RelayCommand]
    public async Task Profile()
    {
        await Application.Current!.MainPage!.Navigation.PushModalAsync(new ProfileModalPage(new ProfileModalViewModel()));
    }

    private static void LogoutInternal()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            WeakReferenceMessenger.Default.Send(new UserLogoutMessage());
        });
    }
}