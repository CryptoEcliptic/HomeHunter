using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HomeHunter.Common;
using HomeHunter.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    PublicId = name,
                };

                uploadResult = this.cloudinaryUtilities.Upload(uploadParams);
            }

            if (uploadResult == null)
            {
                throw new InvalidOperationException("Unsuccessful Cloudinary upload!");
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }

        public int DeleteCloudinaryImages(IEnumerable<string> imageIds)
        {
            if (imageIds.Count() != 0)
            {
                var delResParams = new DelResParams()
                {
                    PublicIds = imageIds.ToList(),
                };
              
                var deletionResult = this.cloudinaryUtilities.DeleteResources(delResParams);

                if (deletionResult == null)
                {
                    throw new InvalidOperationException("No images deleted from Cloudinary!");
                }

                return imageIds.Count();
            }

            return 0;
        }
    }
}
