using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime last_activity { get; set; }
        public string UserEnabled { get; set; }
        public int UserDespacho { get; set; } 

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("connect")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("SEGURIDAD"); // Se debe usar en Mayuscula

            //modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            //modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
        }
    }
}