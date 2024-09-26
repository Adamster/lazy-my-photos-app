using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lazy.MyPhotos.App.Infrastructure.Messages.User;

public sealed class UserLogoutMessage : AsyncRequestMessage<LogoutMessage>
{
}

public record LogoutMessage;