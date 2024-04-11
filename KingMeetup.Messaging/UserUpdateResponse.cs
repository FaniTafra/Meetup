using KingMeetup.Messaging.Common;
using KingMeetup.Messaging.Validation;
using KingMeetup.Model;
using KingMeetup.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging
{
    public class UserUpdateResponse : RequestBase
    {
        public int Id { get; set; }
        public Status Status { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.App.FirstName_Required), ErrorMessageResourceType = typeof(Resources.App))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.App.LastName_Required), ErrorMessageResourceType = typeof(Resources.App))]
        public string LastName { get; set; }

        //[PasswordValidation(8, 50, true, true, true, true, ErrorMessage = "Neispravan unos lozinke!")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessageResourceName = nameof(Resources.App.Password_Compare), ErrorMessageResourceType = typeof(Resources.App))]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.App.Email_Required), ErrorMessageResourceType = typeof(Resources.App))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.App.Email_Validation), ErrorMessageResourceType = typeof(Resources.App))]
        [RegularExpression("^[a-zA-Z0-9.+_~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", ErrorMessageResourceName = nameof(Resources.App.Email_Character_Validation), ErrorMessageResourceType = typeof(Resources.App))]
        public string Email { get; set; }

        [PhoneValidation(8, 15, true, ErrorMessage = "Neispravan unos broja telefona!")]
        public string? Phone { get; set; }
        public Role RoleID { get; set; }
        public string? Description { get; set; }
        public string? PictureLocation { get; set; }
        public List<UsersInterest> UsersInterests { get; set; } = new List<UsersInterest>();
        public List<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
