using Lazy.MyPhotos.App.Infrastructure.Authorization;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

public static class ApiServicesContainerExtensions
{
    public static MauiAppBuilder RegisterApiServices(this MauiAppBuilder mauiAppBuilder)
    {

        mauiAppBuilder.Services.AddScoped<AuthorizationHeaderHandler>();

        var refitSettings = new RefitSettings();
        mauiAppBuilder.Services
            .AddRefitClient<IUserApi>(refitSettings)
            .ConfigureHttpClient(cc => cc.BaseAddress = new Uri("https://lazy-photo-api.azurewebsites.net"))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        //TODO: replace with value from settings

        return mauiAppBuilder;
    }
}