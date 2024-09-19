namespace Lazy.MyPhotos.App.Modules.Photo.Models;

public record PhotoItem(long Id, string Filename, ImageSource? Image = null);