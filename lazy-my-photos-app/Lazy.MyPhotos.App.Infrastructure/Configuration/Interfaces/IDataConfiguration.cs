namespace Lazy.MyPhotos.App.Infrastructure.Configuration.Interfaces
{
    public interface IDataConfiguration
    {
        string DbFilePath { get; }

        string BaseWorkingDirectory { get; }

        string TempDirectory { get; }
    }
}