using Refit;

namespace Lazy.MyPhotos.App.Infrastructure.ApiServices;

[Headers("Authorization: Bearer")]
public interface IPhotoContentApi
{
    [Get("/api/PhotoContent/{id}")]
    Task<IApiResponse<Stream>> GetPhoto(long id);
}