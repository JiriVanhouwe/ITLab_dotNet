using ITLab.Models;
using System;
using System.Collections.Generic;

namespace ITLab.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Session = new HashSet<Session>();
        }

        public string Classid { get; set; }
        public Campus Campus { get; set; }
        public string Maxseats { get; set; }
        public RoomCategory Roomcategory { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }
}
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    using System;
    using System.Collections.Generic;

    namespace TestDatabase.Models
    {
        public partial class Classroom
        {
            public Classroom()
            {
                Session = new HashSet<Session>();
            }

            public string Classid { get; set; }
            public string Campus { get; set; }
            public string Maxseats { get; set; }
            public string Roomcategory { get; set; }

            public virtual ICollection<Session> Session { get; set; }
        }
    }

    public enum Campus
    {
        GENT, AALST
    }

    public enum ClassroomCategory
    {
        AUDITORIUM,
        CLASSROOM,
        PCCLASSROOM,
        ITLAB,
        MEETINGROOM,
        LAPTOPCLASSROOM,
        LANGUAGELAB
    }

    public class Classroom
    {
        public Classroom(string id, Campus campus, int maxSeats, ClassroomCategory category)
        {
            this.Id = id;
            this.Campus = campus;
            this.MaxSeats = maxSeats;
            this.Category = category;
        }

        public string Id { get; set; }
        public Campus Campus { get; set; }
        public int MaxSeats { get; set; }
        public ClassroomCategory Category { get; set; }
    }
}
*/
