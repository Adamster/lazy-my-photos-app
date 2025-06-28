using Lazy.MyPhotos.App.Infrastructure.Authorization;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

public static class ApiServicesContainerExtensions
{
    private const string BaseUrl = "https://lazy-photo-api.azurewebsites.net";

    public static MauiAppBuilder RegisterApiServices(this MauiAppBuilder mauiAppBuilder)
    {

        mauiAppBuilder.Services.AddScoped<AuthorizationHeaderHandler>();

        var refitSettings = new RefitSettings();
        mauiAppBuilder.Services
            .AddRefitClient<IUserApi>(refitSettings)
            .ConfigureHttpClient(cc => cc.BaseAddress = new Uri(BaseUrl))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        mauiAppBuilder.Services
            .AddRefitClient<IPhotoApi>(refitSettings)
            .ConfigureHttpClient(cc => cc.BaseAddress = new Uri(BaseUrl))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        mauiAppBuilder.Services
            .AddRefitClient<IPhotoContentApi>(refitSettings)
            .ConfigureHttpClient(cc => cc.BaseAddress = new Uri(BaseUrl))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        //TODO: replace with value from settings

        return mauiAppBuilder;
    }
}