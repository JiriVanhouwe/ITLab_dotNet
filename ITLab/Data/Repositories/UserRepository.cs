using ITLab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ITLab_DBContext _dbContext;
        private readonly DbSet<ItlabUser> _users;


        public UserRepository(ITLab_DBContext context)
        {
            _dbContext = context;
            _users = _dbContext.ItlabUser;
        }

        public ItlabUser GetById()
        {
           return  _users.First();
        }
    }
}
