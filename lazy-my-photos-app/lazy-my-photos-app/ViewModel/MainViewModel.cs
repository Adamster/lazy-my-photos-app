﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lazy.MyPhotos.App.View.User;


namespace Lazy.MyPhotos.App.ViewModel;

[ObservableObject]
public partial class MainViewModel
{
    [RelayCommand]
    private async Task Register()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    [RelayCommand]
    private async Task Login()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}