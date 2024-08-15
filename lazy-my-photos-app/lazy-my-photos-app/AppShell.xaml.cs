using lazy_my_photos_app.View;
using lazy_my_photos_app.View.User;
using Microsoft.Maui.Controls;

namespace lazy_my_photos_app;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}