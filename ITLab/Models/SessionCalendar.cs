using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public class SessionCalendar
    {
        #region Fields
        private string _id;
        private DateTime _startDate;
        private DateTime _endTime;
        #endregion

        #region Properties
        public string Id { get => _id; 
           set {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 8)
                {
                   throw new ArgumentException("id moet bestaan uit schooljaar dus twee jaartallen zoals 20192020");
                }
                    
                int part1 = (int.Parse(value.Substring(0, 4))) + 1;
                int part2 = int.Parse(value.Substring(4));

                if (part1 != part2)
                {
                    throw new ArgumentException("id moet bestaan uit die opeenvolgende jaartallen");
                }
                    
                _id = value;
            } }
        public IEnumerable<Session> Sessions { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        #endregion

        #region Constructor
        public SessionCalendar(string id, IEnumerable<Session> sessions, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Sessions = sessions;
            SetDates(startDate, endDate);

        }


        #endregion

        #region Methodes
        private void SetDates(DateTime startDate, DateTime endDate)
        {
            if (DateTime.Compare(endDate , startDate.AddDays(1)) <= 0)
            {
                throw new ArgumentException("einde datum moet minstens een dag achter begin datum liggen");
            }
           if (startDate.Year == endDate.Year|| startDate.AddYears(1).Year == endDate.Year) 
            {
                StartDate = startDate;
                EndDate = endDate;
            }
            else
            {
                throw new ArgumentException("Datums mogen maximaal 1 jaar verschillen");
            }
           
        }
        #endregion
    }
}
