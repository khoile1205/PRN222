namespace BussinessLayer.Services.Abstraction
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(Stream fileStream, string pathName);
    }
}