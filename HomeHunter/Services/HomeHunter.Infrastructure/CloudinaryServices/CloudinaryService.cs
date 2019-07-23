using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HomeHunter.Infrastructure.CloudinaryServices
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtilities;

        public CloudinaryService(Cloudinary cloudinaryUtilities)
        {
            this.cloudinaryUtilities = cloudinaryUtilities;
        }

        public async Task<string> UploadPictureAsync(IFormFile pictureFile, string name)
        {
            byte[] destinationData;

            using (var memoryStream = new MemoryStream())
            {
                await pictureFile.CopyToAsync(memoryStream);
                destinationData = memoryStream.ToArray();
            }
            UploadResult uploadResult = null;

            using (var memoryStream = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "RealEstates",
                    File = new FileDescription(name, memoryStream),
                };

                uploadResult = this.cloudinaryUtilities.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
