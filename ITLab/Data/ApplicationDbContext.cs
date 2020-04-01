using System;
using System.Collections.Generic;
using System.Text;
using ITLab.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITLab.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> ITLabUsers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionCalendar> SessionCalendars { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
