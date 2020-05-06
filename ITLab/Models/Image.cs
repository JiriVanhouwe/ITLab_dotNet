using System;
using System.Collections.Generic;

namespace ITLab.Models
{
    public partial class Image
    {
        public int Imagekey { get; set; }
        public byte[] Image1 { get; set; }

        public string ToBase64()
        {
            return Convert.ToBase64String(this.Image1);
        }
    }
}
