using ITLab.Models;
using System;
using System.Collections.Generic;

namespace ITLab.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string Contenttext { get; set; }
        public string AuthorUsername {
            get => AuthorUsername;
            private set
            {
                if (value == null)
                    throw new ArgumentException("De schrijver werd niet herkend.");
                AuthorUsername = value;
            }
        }
        public int? SessionId { get; set; }

        public virtual ItlabUser AuthorUsernameNavigation { get; set; }
        public virtual Session Session { get; set; }
        public Feedback(ItlabUser author, string content)
        {
            AuthorUsernameNavigation = author;
            AuthorUsername = author.Username;
            Contenttext = content;
        }

        protected Feedback()
        {

        }

    }
}


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public class Feedback
    {
        public User Author { get => Author; 
            set 
            {
                if (value == null)
                    throw new ArgumentException("De schrijver werd niet herkend.");
                Author = value;
            } 
        }
        public string Content { get => Content; set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Gelieve feedback in te geven.");

                Content = value;
            } 
        }

        public Feedback(User author, string content)
        {
            Author = author;
            Content = content;
        }
    }
}*/
