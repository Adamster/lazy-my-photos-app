using Microsoft.Maui.Controls;

namespace lazy_my_photos_app;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}