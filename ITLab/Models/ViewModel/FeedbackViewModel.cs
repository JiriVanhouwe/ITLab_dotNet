using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models.ViewModel
{
    public class FeedbackViewModel
    {
        public String Title { get; set; }
        [Required(ErrorMessage = "Gelieve feedback in te vullen.")]
        public string Feedback { get; set; }
    }
}
