using System;
using System.Collections.Generic;

namespace ITLab.Models
{
    public partial class AttendeeUser
    {
        public int SessionId { get; set; }
        public string UserUsername { get; set; }

        public virtual Session Session { get; set; }
        public virtual ItlabUser UserUsernameNavigation { get; set; }


        public AttendeeUser()
        {

        }

        public AttendeeUser(Session session, ItlabUser user)
        {
            Session = session;
            SessionId = session.Id;
            UserUsernameNavigation = user;
            UserUsername = user.Username;
        }
    }


}
