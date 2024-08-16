using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.Extensions;

public static class ApiServicesContainerExtensions
{
    public static MauiAppBuilder RegisterApiServices(this MauiAppBuilder mauiAppBuilder)
    {

        var refitSettings = new RefitSettings();
        mauiAppBuilder.Services
            .AddRefitClient<IUserApi>(refitSettings)
            .ConfigureHttpClient(cc => cc.BaseAddress = new Uri("https://localhost:7258"));

        //TODO: replace with value from settings

        return mauiAppBuilder;
    }
}