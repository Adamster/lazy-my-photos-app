using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.Messages.User
{
    internal class UserLoggedInMessage : ValueChangedMessage<LoginResponse>
    {
        public UserLoggedInMessage(LoginResponse value) : base(value)
        {
        }
    }
}
