using System;
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
}
