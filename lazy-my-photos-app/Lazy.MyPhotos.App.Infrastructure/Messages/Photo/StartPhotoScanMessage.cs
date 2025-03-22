using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazy.MyPhotos.App.Infrastructure.Messages.Photo
{
    public sealed class StartPhotoScanMessage : AsyncRequestMessage<PhotoScanMessage>
    {
    }

    public record PhotoScanMessage;
}
