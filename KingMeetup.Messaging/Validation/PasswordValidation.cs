using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging.Validation
{
    public class PasswordValidation : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;
        private readonly bool _requireDigit;
        private readonly bool _requireLowercase;
        private readonly bool _requireUppercase;
        private readonly bool _requireNonAlphanumeric;

        public PasswordValidation(int minLength, int maxLength, bool requireDigit, bool requireLowercase, bool requireUppercase, bool requireNonAlphanumeric)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            _requireDigit = requireDigit;
            _requireLowercase = requireLowercase;
            _requireUppercase = requireUppercase;
            _requireNonAlphanumeric = requireNonAlphanumeric;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value as string;

            if (password.Length < _minLength || password.Length > _maxLength)
                return new ValidationResult($"Lozinka mora imati minimalno {_minLength} i maksimalno {_maxLength} znakova.", new[] { validationContext.MemberName });

            if (_requireDigit && !password.Any(char.IsDigit))
                return new ValidationResult("Lozinka mora sadržavati barem jedan broj.", new[] { validationContext.MemberName });

            if (_requireLowercase && !password.Any(char.IsLower))
                return new ValidationResult("Lozinka mora sadržavati barem jedno malo slovo.", new[] { validationContext.MemberName });

            if (_requireUppercase && !password.Any(char.IsUpper))
                return new ValidationResult("Lozinka mora sadržavati barem jedno veliko slovo.", new[] { validationContext.MemberName });

            if (_requireNonAlphanumeric && !password.Any(c => !char.IsLetterOrDigit(c)))
                return new ValidationResult("Lozinka mora sadržavati barem jedan specijalni znak.", new[] { validationContext.MemberName });

            return ValidationResult.Success;
        }
    }
}
