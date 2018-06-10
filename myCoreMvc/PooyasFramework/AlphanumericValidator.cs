using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PooyasFramework
{
    public class AlphanumericValidator : ValidationAttribute
    {
        [Range(1, int.MaxValue)]
        private int _minLength = 0;
        [Range(1, int.MaxValue)]
        private int _maxLength;
        private bool _mustStartWithLetter = false;
        private bool _mustStartWithCapital = false;

        public AlphanumericValidator( int minLength = 1, int maxLength = 1, bool mustStartWithLetter = true, bool mustStartWithCapital = true)
        {
            if (minLength <= 0 )
                throw new ValidationException("Minimum length must be greater than zero."); // For developer
            if (maxLength <= minLength)
                throw new ValidationException("Maximum length must be greater than minimum length."); // For developer
            _minLength = minLength;
            _maxLength = maxLength;
            _mustStartWithLetter = mustStartWithLetter;
            _mustStartWithCapital = mustStartWithCapital;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult($"{validationContext.DisplayName} may not be empty.");

            var stringValue = (string)value;

            if (stringValue.Length < _minLength)
                return new ValidationResult($"{validationContext.DisplayName} must be at least {_minLength} characters long.");

            if (stringValue.Length > _maxLength)
                return new ValidationResult($"{validationContext.DisplayName} may not exceed {_maxLength} characters length.");

            if (_mustStartWithLetter && Regex.Match(stringValue, "^[a-zA-Z].*").Success == false)
                return new ValidationResult($"{validationContext.DisplayName} must start with a letter.");

            if (_mustStartWithCapital && Regex.Match(stringValue, "^[A-Z].*").Success == false)
                return new ValidationResult($"{validationContext.DisplayName} must start with a capital.");

            if (Regex.Match(stringValue, "^[a-zA-Z0-9]*$").Success)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{validationContext.DisplayName} may only contain alphanumeric characters.");
            }
        }
    }
}
