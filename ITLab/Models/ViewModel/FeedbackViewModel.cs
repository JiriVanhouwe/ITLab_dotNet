using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models.ViewModel
{
    public class FeedbackViewModel
    {

        [Required(ErrorMessage = "Gelieve feedback in te vullen.")]
        public string Feedback { get; set; }


        public FeedbackViewModel(string feedback)
        {
            Feedback = feedback;
        }
    }
}
