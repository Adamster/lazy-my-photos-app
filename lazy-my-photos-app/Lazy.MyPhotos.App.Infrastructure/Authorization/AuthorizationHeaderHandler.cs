using System.Net.Http.Headers;
using Lazy.MyPhotos.App.Infrastructure.Services;

namespace Lazy.MyPhotos.App.Infrastructure.Authorization;

public class AuthorizationHeaderHandler : DelegatingHandler
{
    private readonly ISettingsService _settingsService;

    public AuthorizationHeaderHandler(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _settingsService.AuthAccessToken;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return base.SendAsync(request, cancellationToken);
    }
}