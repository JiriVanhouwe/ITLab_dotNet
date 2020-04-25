using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Models
{
    public interface IUserRepository
    {
        public ItlabUser LoggedInUser { get; set; }
        ItlabUser GetById();
        List<ItlabUser> GetAllUsers();
    }
}
