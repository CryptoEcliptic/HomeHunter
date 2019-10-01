using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeHunter.Infrastructure.ValidationAttributes
{
    public class BeforeCurrentYearAttribute : ValidationAttribute
    {
        private const int MaxYearsAhead = 3;
        private readonly int minYear;

        public BeforeCurrentYearAttribute(int minYear)
        {
            this.minYear = minYear;
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
            if (intValue > DateTime.UtcNow.Year + MaxYearsAhead)
            {
                return new ValidationResult(validationContext?.DisplayName + "та не може да бъде по-голяма от " + (DateTime.UtcNow.Year + MaxYearsAhead));
            }

            if (intValue < minYear)
            {
                return new ValidationResult(validationContext?.DisplayName + "та не може да бъде преди " + minYear);
            }

            return ValidationResult.Success;
        }
    }
}
