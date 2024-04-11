using KingMeetup.Messaging.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Messaging
{
    public class UsersInterestRequest : RequestBase
    {
        public int UserId { get; set; }
        public int InterestId { get; set; }
    }
}
