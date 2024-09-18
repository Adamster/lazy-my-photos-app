using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lazy.MyPhotos.App.Messages.User;

public class UserLogoutMessage : AsyncRequestMessage<LogoutMessage>
{
}

public record LogoutMessage();