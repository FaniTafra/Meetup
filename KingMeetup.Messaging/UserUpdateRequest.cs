using KingMeetup.Messaging.Common;
using System.ComponentModel.DataAnnotations;

namespace KingMeetup.Messaging
{
    public class UserUpdateRequest : RequestBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}

