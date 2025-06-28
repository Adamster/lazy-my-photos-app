using System.Net;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.Persistence;
using Lazy.MyPhotos.Persistence.Entities;
using Lazy.MyPhotos.Shared.Models.User;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Impl;

public class UserService(IUserApi userApi, ILogger<UserService> logger, PhotoDbContext dbContext) : IUserService
{
    private readonly PhotoDbContext _dbContext = dbContext;

    public async Task<UserModel?> GetUser()
    {
        var user = await userApi.GetUser();

        if (user.StatusCode == HttpStatusCode.Unauthorized)
        {
            //try refresh token later , return null will logout
            logger.LogWarning("Unauthorized response, user eviction");
            
        }

        if (user is { IsSuccessStatusCode: true, Content: not null })
        {
            var lazyUser = new User
            {
                Email = user.Content.Email, UserName = user.Content.UserName, UserId = user.Content.Id
            };


            var userFromDb = await _dbContext.GetItemByIdAsync<User>(lazyUser.UserId);
            if (userFromDb != null && userFromDb.UserId != lazyUser.UserId)
            {
                await _dbContext.AddItemAsync(lazyUser);
            }

        }


        return user.IsSuccessStatusCode ? user.Content : null;
    }
}