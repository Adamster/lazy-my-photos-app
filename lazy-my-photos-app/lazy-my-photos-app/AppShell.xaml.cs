

using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.View.User;
using Lazy.MyPhotos.App.ViewModel;

namespace Lazy.MyPhotos.App;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

      

    }
}