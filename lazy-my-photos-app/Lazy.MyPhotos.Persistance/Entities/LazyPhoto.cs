namespace Lazy.MyPhotos.Persistence.Entities
{
    public class LazyPhoto
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string Path { get; init; } = null!;

        public string? Hash { get; init; }

        public string? UploadHashResult { get; set; }

        public bool IsSynced { get; init; }
    }
}