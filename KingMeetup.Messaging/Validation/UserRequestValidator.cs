using FluentValidation;
using System.Text.RegularExpressions;

namespace KingMeetup.Messaging.Validation
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {    
        public UserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email ne smije biti prazan.")
                .EmailAddress().WithMessage("Mora biti valjana email adresa.")
                .Length(1, 50).WithMessage("Email moze imati najviše 50 znakova.");             
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Nije unesena lozinka.")
                .Length(1, 50).WithMessage("Lozinka moze imati najviše 50 znakova.")
                .Must(HasNonAlphanumeric).WithMessage("Lozinka mora imati bar 1 ne alfanumericki znak.")
                .Must(HasDigit).WithMessage("Lozinka mora imati bar 1 broj.")
                .Must(HasUppercase).WithMessage("Lozinka mora imati bar jedno veliko slovo.")
                .Must(HasLowerCase).WithMessage("Lozinka mora imati bar jedno malo slovo.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ime ne smije biti prazno.")
                .Length(1, 50).WithMessage("Ime moze imati najviše 50 znakova.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Prezime ne smije biti prazno.")
                .Length(1, 50).WithMessage("Ime moze imati najviše 50 znakova.");
            RuleFor(x => x.Phone)
                .Must(phone => string.IsNullOrEmpty(phone) || BeANumber(phone)).WithMessage("Broj telefona mora biti broj.")
                .Length(0, 64).WithMessage("Broj ne smije imati više od 64 znaka.");
        }
        private bool HasNonAlphanumeric(string str)
        {
            Regex regex = new Regex(@"\W");
            return regex.IsMatch(str);
        }
        private bool HasDigit(string str)
        {
            Regex regex = new Regex(@"\d");
            return regex.IsMatch(str);
        }
        private bool HasUppercase(string str)
        {
            Regex regex = new Regex("[A-Z]");
            return regex.IsMatch(str);
        }
        private bool HasLowerCase(string str)
        {
            Regex regex = new Regex("[a-z]");
            return regex.IsMatch(str);
        }
        private bool BeANumber(string str)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(str);
        }
    }
}
