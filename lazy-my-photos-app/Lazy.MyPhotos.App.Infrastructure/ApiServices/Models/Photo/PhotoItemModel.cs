using Lazy.MyPhotos.App.Infrastructure.ApiServices.Models.Photo;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

public class PhotoItemModel
{
    public int Id { get; set; }
    public string DisplayFileName { get; set; }
    public string PhotoUrl { get; set; }
    public string BlobId { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public PhotoMetadata PhotoMetadata { get; set; }
}