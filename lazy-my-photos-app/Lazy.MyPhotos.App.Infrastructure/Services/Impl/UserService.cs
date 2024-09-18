using System.Net;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.Shared.Models.User;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Impl;

public class UserService(IUserApi userApi, ILogger<UserService> logger) : IUserService
{
    public async Task<UserModel?> GetUser()
    {
        var user = await userApi.GetUser();

        if (user.StatusCode == HttpStatusCode.Unauthorized)
        {
            //try refresh token later , return null will logout
            logger.LogWarning("Unauthorized response, user eviction");
            
        }

        return user.IsSuccessStatusCode ? user.Content : null;
    }
}