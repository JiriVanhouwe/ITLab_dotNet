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

    public enum ClassRoomCategory
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
        public string Id { get; set; }
        public Campus Campus { get; set; }
        public int MaxSeats { get; set; }
        public ClassRoomCategory RoomCategory { get; set; }
    }
}
