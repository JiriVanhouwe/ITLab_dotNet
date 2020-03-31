using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
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
