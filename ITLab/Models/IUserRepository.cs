using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public interface IUserRepository
    {
        public static ItlabUser LoggedInUser { get; set; }
        ItlabUser GetById(string id); //id is de userName = string
        List<ItlabUser> GetAllUsers();

        void SaveChanges();
    }
}
