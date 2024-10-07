using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Lazy.MyPhotos.App.Infrastructure.Exceptions.Config
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public sealed class ConfigurationMissingException(string key) : Exception($"{key} key is missing from configuration");
}