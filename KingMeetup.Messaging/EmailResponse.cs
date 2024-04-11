using KingMeetup.Messaging.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Messaging
{
    public class EmailResponse
    {
        //TODO: Naslijedi responsebase klasu
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
