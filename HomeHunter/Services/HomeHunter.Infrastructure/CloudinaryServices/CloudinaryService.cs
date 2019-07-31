using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HomeHunter.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            string fileExtension = System.IO.Path.GetExtension(pictureFile.FileName);

            if (!GlobalConstants.AllowedFileExtensions.Contains(fileExtension))
            {
                throw new FormatException("Provided image format not supproted!");
            }

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
