using Lazy.MyPhotos.Shared.Models.User;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

public interface IUserApi
{
    [Post("/login")]
    Task<IApiResponse<LoginResponse>> Login([Body]LoginRequest loginModel);

    [Post("/register")]
    Task<IApiResponse> Register([Body]RegisterRequest registerModel);
}