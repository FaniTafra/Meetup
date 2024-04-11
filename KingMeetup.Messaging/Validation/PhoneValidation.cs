using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging.Validation
{
    public class PhoneValidation : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;
        private readonly bool _allDigit;
 

        public PhoneValidation(int minLength, int maxLength, bool allDigit)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            _allDigit = allDigit;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string phone = value as string;

            if (phone == null || String.IsNullOrEmpty(phone.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {

                if (phone.Length < _minLength || phone.Length > _maxLength)
                    return new ValidationResult($"Broj telefona mora imati minimalno {_minLength} i maksimalno {_maxLength} brojeva.", new[] { validationContext.MemberName });

                if (_allDigit && !phone.All(char.IsDigit))
                    return new ValidationResult("Nije moguće unijeti druge znakove osim brojeva!", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
