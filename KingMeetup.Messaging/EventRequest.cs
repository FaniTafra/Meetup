using KingMeetup.Messaging.Common;
using KingMeetup.Messaging.Validation;
using KingMeetup.Model;
using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging
{
    public class EventRequest : RequestBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naziv ne smije biti prazan.")]
        [StringLength(50,ErrorMessage ="Naziv ne smije imati više od 50 znakova.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Pocetak ne smije biti prazan.")]
        public DateTime StartDateTime { get; set; }
        [EndDateTimeValidation("StartDateTime")]
        public DateTime EndDateTime { get; set; } 
        public int UserId { get; set; }
        [Required,Range(1, int.MaxValue, ErrorMessage = "Morate odabrati kategoriju.")]
        public int InterestId { get; set; }
        [Required(ErrorMessage = "Morate postaviti broj polaznika uživo."), Range(1,100,ErrorMessage ="Broj polaznika uživo mora biti veći od 0 i manji od 100.")]
        public int AttendeesOnSite { get; set; }
        [Required(ErrorMessage = "Morate postaviti broj polaznika online."), Range(1, 100, ErrorMessage = "Broj polaznika online mora biti veći od 0 i manji od 100.")]
        public int AttendeesOnLine { get; set; }
        [Required(ErrorMessage = "Adresa ne smije biti prazna!")]
        [StringLength(255, ErrorMessage = "Adresa ne smije imati više od 255 znakova.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Morate odabrati grad.")]
        [StringLength(100, ErrorMessage = "Grad ne smije imati više od 100 znakova.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Morate odabrati državu.")]
        [StringLength(100, ErrorMessage = "Država ne smije imati više od 100 znakova!")]
        public string State { get; set; }
        [Required(ErrorMessage ="Opis ne smije biti prazan.")]
        [StringLength(1000, ErrorMessage = "Opis ne smije imati više od 1000 znakova.")]
        public string Description { get; set; }
        public string? UserToken { get; set; }
    }
}
