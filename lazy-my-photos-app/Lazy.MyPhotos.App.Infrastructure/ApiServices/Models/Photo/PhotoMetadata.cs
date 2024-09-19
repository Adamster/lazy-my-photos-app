namespace Lazy.MyPhotos.App.Infrastructure.ApiServices.Models.Photo;

public class PhotoMetadata
{
    public string CameraModel { get; set; }
    public string Aperture { get; set; }
    public string ShutterTime { get; set; }
    public int FocusRange { get; set; }
    public int IsoCount { get; set; }
}