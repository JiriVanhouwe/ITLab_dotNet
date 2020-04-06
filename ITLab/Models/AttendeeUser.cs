using System;
using System.Collections.Generic;

namespace TestDatabase.Models
{
    public partial class AttendeeUser
    {
        public int SessionId { get; set; }
        public string UserUsername { get; set; }

        public virtual Session Session { get; set; }
        public virtual ItlabUser UserUsernameNavigation { get; set; }
    }
}
