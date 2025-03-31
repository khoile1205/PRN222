namespace BussinessLayer.Services.Abstraction
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(Stream fileStream, string path);
        Task<bool> DeleteImage(string imageUrl);
    }
}