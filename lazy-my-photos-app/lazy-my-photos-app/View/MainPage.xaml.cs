using System;
using lazy_my_photos_app.ViewModel;
using Microsoft.Maui.Accessibility;
using Microsoft.Maui.Controls;

namespace lazy_my_photos_app.View;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel = new();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = _viewModel;
    }
}