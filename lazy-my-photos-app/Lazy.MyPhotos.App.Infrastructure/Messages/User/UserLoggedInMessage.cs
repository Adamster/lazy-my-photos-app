using CommunityToolkit.Mvvm.Messaging.Messages;
using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.Infrastructure.Messages.User
{
    public sealed class UserLoggedInMessage(LoginResponse value) : ValueChangedMessage<LoginResponse>(value)
    {
    }
}
