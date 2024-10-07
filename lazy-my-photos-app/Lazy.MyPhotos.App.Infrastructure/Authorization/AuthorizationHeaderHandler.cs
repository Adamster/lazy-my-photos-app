using System.Net;
using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.Messages.User;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.Shared.Models.User;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Infrastructure.Authorization;

public class AuthorizationHeaderHandler : DelegatingHandler
{
    private readonly ISettingsService _settingsService;
    private readonly ILogger<AuthorizationHeaderHandler> _logger;
    private const string BearerHeaderKey = "Bearer";

    public AuthorizationHeaderHandler(ISettingsService settingsService, ILogger<AuthorizationHeaderHandler> logger)
    {
        _settingsService = settingsService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _settingsService.AuthAccessToken;
        var refreshToken = _settingsService.RefreshToken;

        request.Headers.Authorization = GenerateHeadersAuthorization(token);

        _logger.LogInformation("Sending request: {0}", request.RequestUri);

        var result =  await base.SendAsync(request, cancellationToken);
        _logger.LogInformation("Request result: success {0} : {1}", result.IsSuccessStatusCode, result.StatusCode);
        if (result is {IsSuccessStatusCode: false, StatusCode: HttpStatusCode.Unauthorized})
        {
            _logger.LogInformation("Access token expired, trying to refresh");
            string? newAccessToken = await TryRefreshToken(refreshToken);
            
            if (newAccessToken is null)
            {
                return result;
            }

            request.Headers.Authorization = GenerateHeadersAuthorization(newAccessToken);

            var newResult = await base.SendAsync(request, cancellationToken);

            if (newResult is {IsSuccessStatusCode:false, StatusCode:HttpStatusCode.Unauthorized})
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    WeakReferenceMessenger.Default.Send(new UserLogoutMessage());
                });
            }
        }

        return result;
    }

    private static AuthenticationHeaderValue GenerateHeadersAuthorization(string token)
    {
        return new AuthenticationHeaderValue(BearerHeaderKey, token);
    }

    private async Task<string?> TryRefreshToken(string refreshToken)
    {
        var userApi = Application.Current!.Handler!.MauiContext!.Services.GetService<IUserApi>();
        var refreshResult = await userApi!.RefreshToken(new RefreshTokenRequest(refreshToken));

        if (refreshResult is { IsSuccessStatusCode: false, StatusCode: HttpStatusCode.Unauthorized })
        {
            _logger.LogWarning("Refresh access token failed");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _logger.LogWarning("Trying to auto logout...");
                WeakReferenceMessenger.Default.Send(new UserLogoutMessage());
            });

            return null;
        }

        if (refreshResult is { IsSuccessStatusCode: true, StatusCode: HttpStatusCode.OK })
        {
           await _settingsService.SaveLoginResponse(refreshResult.Content!);
        }

        return refreshResult.Content!.AccessToken;
    }
}