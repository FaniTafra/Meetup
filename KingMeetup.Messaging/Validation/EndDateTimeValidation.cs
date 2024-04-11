using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging.Validation
{
    public class EndDateTimeValidationAttribute : ValidationAttribute
    {
        private readonly string _startDateTime;

        public EndDateTimeValidationAttribute(string startDateTime)
        {
            _startDateTime = startDateTime;
        }
        public string GetErrorMessage() => "Kraj događaja ne može biti prije početka događaja.";
        protected override ValidationResult? IsValid(object? kraj, ValidationContext validationContext)
        {
            var pocetak = validationContext.ObjectType.GetProperty(_startDateTime);

            var krajString = kraj != null ? kraj.ToString() : null;

            if (string.IsNullOrWhiteSpace(krajString))
            {
                return ValidationResult.Success;
            }

            if (kraj == null)
            {
                return new ValidationResult("Odredite datum kraja.");
            }

            if (!DateTime.TryParse(krajString, out DateTime krajValue))
            {
                return new ValidationResult("Kraj je unesen u krivom formatu.");
            }

            var pocetakValue = pocetak.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (krajValue == null)
            {
                return ValidationResult.Success;
            }

            if (krajValue < pocetakValue)
            {
                return new ValidationResult(GetErrorMessage(), new[] {validationContext.MemberName });
            }

            return ValidationResult.Success;

        }
    }
}
