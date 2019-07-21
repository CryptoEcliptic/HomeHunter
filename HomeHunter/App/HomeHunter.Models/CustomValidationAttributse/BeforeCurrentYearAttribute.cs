using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.CustomValidationAttributse
{
    public class BeforeCurrentYearAttribute : ValidationAttribute
    {
        private readonly int afterYear;

        public BeforeCurrentYearAttribute(int afterYear)
        {
            this.afterYear = afterYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value is null))
            {
                return ValidationResult.Success;
            }

            if (!(value is int))
            {
                return new ValidationResult("Invalid " + validationContext?.DisplayName);
            }

            var intValue = (int?)value;
            if (intValue > DateTime.UtcNow.Year)
            {
                return new ValidationResult(validationContext?.DisplayName + "та не може да бъде по-голяма от " + DateTime.UtcNow.Year);
            }

            if (intValue < afterYear)
            {
                return new ValidationResult(validationContext?.DisplayName + "та не може да бъде преди " + afterYear);
            }

            return ValidationResult.Success;
        }
    }
}
