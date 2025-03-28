using BussinessLayer.Services.Abstraction;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImage(Stream fileStream, string pathName)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(Guid.NewGuid().ToString(), fileStream),
                Folder = pathName,
                Transformation = new Transformation().Width(300).Height(300).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}