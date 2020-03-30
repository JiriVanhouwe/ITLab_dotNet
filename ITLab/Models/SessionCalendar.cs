﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public class SessionCalendar
    {
        public IEnumerable<Session> Sessions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
