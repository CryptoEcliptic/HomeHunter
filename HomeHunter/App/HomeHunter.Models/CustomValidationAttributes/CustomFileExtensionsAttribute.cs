using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace HomeHunter.Models.CustomValidationAttributse
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CustomFileExtensionsAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public CustomFileExtensionsAttribute(string allowedExtensions)
        {
            AllowedExtensions = allowedExtensions.Split(new string[] {", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            foreach (var item in (List<IFormFile>)value)
            {
                IFormFile file = item as IFormFile;

                if (file != null)
                {
                    var fileName = file.FileName;

                    var result = AllowedExtensions.Any(y => fileName.EndsWith(y));
                    if (!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
