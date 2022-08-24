using GymApp.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<GymClass> GymClasses => Set<GymClass>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserGymClass>()
                   .HasKey(a => new { a.ApplicationUserId, a.GymClassId });
        }
    }

    //Install-Package Microsoft.EntityFrameworkCore.Tools -Version 6.0.8

    //public class ApplicationDbContext : IdentityDbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //        : base(options)
    //    {
    //    }
    //}
}