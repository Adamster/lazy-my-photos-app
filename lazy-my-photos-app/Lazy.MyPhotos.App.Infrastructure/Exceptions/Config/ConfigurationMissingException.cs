using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Lazy.MyPhotos.App.Infrastructure.Exceptions.Config
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public sealed class ConfigurationMissingException : Exception
    {
        public ConfigurationMissingException(string key) : base($"{key} key is missing from configuration")
        {
        }

        private ConfigurationMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}