using Lazy.MyPhotos.App.Infrastructure.ApiServices.Models.Photo;
using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

[Headers("Authorization: Bearer")]
public interface IPhotoApi
{
    [Get("/api/Photo")]
    Task<IApiResponse<PhotoItemModel[]>> GetPhotos();
}