using KingMeetup.Messaging.Common;
using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging
{
    public class LoginRequest : RequestBase
    {
        [Required(ErrorMessageResourceName = nameof(Resources.App.Email_Required), ErrorMessageResourceType = typeof(Resources.App))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.App.Email_Validation), ErrorMessageResourceType = typeof(Resources.App))]
        [RegularExpression("^[a-zA-Z0-9.+_~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", ErrorMessageResourceName = nameof(Resources.App.Email_Character_Validation), ErrorMessageResourceType = typeof(Resources.App))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.App.Password_Required), ErrorMessageResourceType = typeof(Resources.App))]
        public string Password { get; set; }
    }
}
