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
        public ItlabUser LoggedInUser { get; set; }

        private readonly ITLab_DBContext _dbContext;
        private readonly DbSet<ItlabUser> _users;

        public UserRepository(ITLab_DBContext context)
        {
            _dbContext = context;
            _users = context.ItlabUser;
        }

        public ItlabUser GetById()
        {
           return  _users.First();
        }

        public List<ItlabUser> GetAllUsers()
        {
            return this._users.ToList();
        }
    }
}
