using BussinessLayer.Services.Abstraction;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace BussinessLayer.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Regex CLOUDINARY_REGEX = new Regex(
            @"^.+\.cloudinary\.com\/(?:[^\/]+\/)?(?:(image|video|raw)\/)?(?:(upload|fetch|private|authenticated|sprite|facebook|twitter|youtube|vimeo)\/)?(?:(?:[^_/]+_[^,/]+,?)*\/)?(?:v(\d+|\w{1,2})\/)?([^.^\s]+)(?:\.(.+))?$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

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

        public async Task<bool> DeleteImage(string url)
        {
            var publicId = this.ExtractPublicId(url);
            var deleteParam = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParam);
            
            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }

            return result.Result.Equals("ok", StringComparison.CurrentCultureIgnoreCase);
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

        private string ExtractPublicId(string link)
        {
            if (string.IsNullOrEmpty(link))
            {
                return string.Empty;
            }

            var match = CLOUDINARY_REGEX.Match(link);
            return match.Success && match.Groups.Count > 4 ? match.Groups[match.Groups.Count - 2].Value : link;
        }
    }
}