using Lazy.MyPhotos.App.Infrastructure.Configuration.Interfaces;

namespace Lazy.MyPhotos.App.Infrastructure.Configuration.Impl
{
    public class DataConfiguration(string dbFilePath, string baseWorkingDirectory, string tempDirectory) : IDataConfiguration
    {
        public string DbFilePath { get; } = dbFilePath;
        public string BaseWorkingDirectory { get; } = baseWorkingDirectory;
        public string TempDirectory { get; } = tempDirectory;
    }
}