using Lazy.MyPhotos.App.Infrastructure.Authorization;
using Lazy.MyPhotos.App.Infrastructure.Exceptions.Config;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

public static class ApiServicesContainerExtensions
{
    public static MauiAppBuilder RegisterApiServices(this MauiAppBuilder mauiAppBuilder)
    {
        var apiUrl = mauiAppBuilder.Configuration["ApiConfig:url"];

        if (string.IsNullOrEmpty(apiUrl))
        {
            throw new ConfigurationMissingException("ApiConfig:url");
        }

        mauiAppBuilder.Services.AddScoped<AuthorizationHeaderHandler>();

        var refitSettings = new RefitSettings();
        
        mauiAppBuilder.Services
            .AddRefitClient<IUserApi>(refitSettings)
            .ConfigureHttpClient(cc => BuildApiBaseAddress(cc, apiUrl))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        mauiAppBuilder.Services
            .AddRefitClient<IPhotoApi>(refitSettings)
            .ConfigureHttpClient(cc => BuildApiBaseAddress(cc, apiUrl))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        mauiAppBuilder.Services
            .AddRefitClient<IPhotoContentApi>(refitSettings)
            .ConfigureHttpClient(cc => BuildApiBaseAddress(cc, apiUrl))
            .AddHttpMessageHandler<AuthorizationHeaderHandler>();

        return mauiAppBuilder;
    }

    private static Uri BuildApiBaseAddress(HttpClient cc, string apiUrl)
    {
        return cc.BaseAddress = new Uri(apiUrl);
    }
}