

using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Messages.User;
using Lazy.MyPhotos.App.View;

namespace Lazy.MyPhotos.App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();

        WeakReferenceMessenger.Default.Register<UserLoggedInMessage>(this, UserLoggedInHandler);
    }

    private void UserLoggedInHandler(object recipient, UserLoggedInMessage message)
    {
        MainPage = new AppShell();
    }
}