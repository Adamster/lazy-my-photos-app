using Lazy.MyPhotos.Shared.Models.User;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;


public interface IUserApi
{
    [Post("/login")]
    Task<IApiResponse<LoginResponse>> Login([Body]LoginRequest loginModel);

    [Post("/refresh")]
    Task<IApiResponse<LoginResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest);

    [Post("/register")]
    Task<IApiResponse> Register([Body]RegisterRequest registerModel);

    [Headers("Authorization: Bearer")]
    [Get("/api/user/currentUser")]
    Task<IApiResponse<UserModel>> GetUser();
}