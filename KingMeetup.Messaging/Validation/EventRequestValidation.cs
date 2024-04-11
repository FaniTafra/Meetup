using FluentValidation;
using System.Text.RegularExpressions;

namespace KingMeetup.Messaging.Validation
{
    public class EventRequestValidation : AbstractValidator<EventRequest>
    {
        public EventRequestValidation()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Naziv događaja ne smije biti prazan")
                .Length(1,50).WithMessage("Naziv ne može imati više od 50 znakova.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adresa događaja ne smije biti prazna")
                .Length(1, 255).WithMessage("Adresa ne može imati više od 255 znakova.");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Grad događaja ne smije biti prazan")
                .Length(1, 100).WithMessage("Grad ne može imati više od 100 znakova.");
            RuleFor(x => x.State)
                .NotEmpty().WithMessage("Država događaja ne smije biti prazana")
                .Length(1, 100).WithMessage("Država ne može imati više od 100 znakova.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Opis događaja ne smije biti prazan")
                .Length(1, 1000).WithMessage("Opis ne može imati više od 1000 znakova.");
            RuleFor(x => x.AttendeesOnLine)
                .NotEmpty().WithMessage("Broj sudionika online ne smije biti prazan.");

            RuleFor(x => x.AttendeesOnSite)
                .NotEmpty().WithMessage("Broj sudionika uživo ne smije biti prazan.");             
        }
    }
}
