

using Lazy.MyPhotos.App.Modules.Photo.Mvvm.ViewModels;
using Lazy.MyPhotos.App.ViewModel;
using Lazy.MyPhotos.App.ViewModel.User;

namespace Lazy.MyPhotos.App.Extensions;

public static class ViewModelsContainerExtensions
{
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainViewModel>();

        //user
        mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        mauiAppBuilder.Services.AddSingleton<RegisterViewModel>();
        

        //photo
        mauiAppBuilder.Services.AddSingleton<PhotoGalleryViewModel>();


        return mauiAppBuilder;
    }
}