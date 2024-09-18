using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel?> GetUser();
    }
}
