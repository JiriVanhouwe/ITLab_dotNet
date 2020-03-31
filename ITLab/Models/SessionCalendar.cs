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
        private IEnumerable<Session> _sessions;
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
                    
                _id = Id;
            } }
        public IEnumerable<Session> Sessions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region Constructor
        public SessionCalendar(string id, IEnumerable<Session> sessions, DateTime startTime, DateTime endTime)
        {
            Id = id;
            Sessions = sessions;
            StartDate = startTime;
            EndDate = endTime;
            
        }
        #endregion
    }
}
