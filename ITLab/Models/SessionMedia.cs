﻿using System;
using System.Collections.Generic;

namespace ITLab.Models
{
    public partial class SessionMedia
    {
        public int? SessionId { get; set; }
        public int? Media { get; set; }

        public virtual Session Session { get; set; }
    }
}
