

using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.View.User;

namespace Lazy.MyPhotos.App;

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