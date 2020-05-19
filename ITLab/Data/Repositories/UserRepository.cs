using ITLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static ItlabUser LoggedInUser { get; set; }

        private readonly ITLab_DBContext _dbContext;
        private readonly DbSet<ItlabUser> _users;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ITLab_DBContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _users = context.ItlabUser;

            //These are used to get the current logged in user
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            addLoggedInUser();
        }

        public ItlabUser GetById(string id)
        {
            return _users.FirstOrDefault(u => u.Username == id);
        }

        public List<ItlabUser> GetAllUsers()
        {
            return this._users.ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        private void addLoggedInUser()
        {
            //Because of cookies a user can already be signed in when the application opens,
            //when this is the case, the loggedInUser in this class won't be set. Therefor we have to check manually if this is the case.
            var user = _httpContextAccessor.HttpContext.User;
            IUserRepository.LoggedInUser = GetById(_userManager.GetUserId(user));
        }

        public ItlabUser GetLoggedInUser()
        {
            return LoggedInUser;
        }
    }
}
