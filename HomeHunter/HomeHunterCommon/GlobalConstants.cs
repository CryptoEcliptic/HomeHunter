using System;
using System.Collections.Generic;

namespace HomeHunter.Common
{
    public static class GlobalConstants
    {
        public const string AdministratorRoleName = "Admin";
        public const string UserRoleName = "User";
        public const string CompanyName = "ХоумХънтър ООД";
        public const string CompanyWebSite = "https://home-hunter.bg";
        public const int UtcTimeCompensationZone = 2;
        public const string DateTimeVisualizationFormat = "dd-MM-yyyy HH:mm";
        public static int DefaultRealEstateYear = DateTime.UtcNow.Year;
        public const string OfferTypeSaleName = "Продажба";
        public const string OfferTypeRentName = "Наем";
        public const int ImageUploadLimit = 12;
        public const string DefaultDateTimeDbValue = "0001-01-01 00:00:00.0000000";
        public const string NotAvailableMessage= "n/a";
        public const string PhoneValidationRegex = @"^([0-9]{10})$|^\+[0-9]{3}[0-9]{9}$|^\+[0-9]{3} [0-9]{3} [0-9]{3} [0-9]{3}$|^\+[0-9]{3} [0-9]{9}$|^[0-9]{4} [0-9]{3} [0-9]{3}$";
        public const string AllowedFileExtensionsAsString = ".jpg, .jpeg, .png, .bmp, .gif";
        public const string BoolTrueStringValue = "ДА";
        public const string BoolFalseStringValue = "НЕ";


        public static List<string> AllowedFileExtensions = new List<string>
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".gif"
        };

       

    }
}
