using System;
using System.Collections.Generic;

namespace ITLab.Models
{
 public partial class SessionCalendar
    {
        public SessionCalendar()
        {
            Session = new HashSet<Session>();
        }

        public int Id { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime? Startdate { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }
}
