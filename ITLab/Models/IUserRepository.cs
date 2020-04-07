using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDatabase.Models;

namespace ITLab.Models
{
    public interface IUserRepository
    {

        ItlabUser GetById();
    }
}
