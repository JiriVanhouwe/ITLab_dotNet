using DataAnnotationsExtensions;

using System.ComponentModel.DataAnnotations;

namespace ITLab.Models.ViewModel
{
    public class UserAttendViewModel
    {
        public Session Session { get; set; }

        [Email]
        [Required]
        public string Email { get; set; }
    }
}
