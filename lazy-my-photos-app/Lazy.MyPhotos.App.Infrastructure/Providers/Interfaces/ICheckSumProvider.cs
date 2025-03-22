namespace Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces
{
    public interface ICheckSumProvider
    {
        byte[] CalculateChecksum(byte[] bytes);
    }
}
