using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using Web.Models;

[assembly: OwinStartupAttribute(typeof(Web.Startup))]
namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {

            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Administrador"))
            {
                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);

                var role2 = new IdentityRole();
                role2.Name = "Magistrado";
                roleManager.Create(role2);

                var role3 = new IdentityRole();
                role3.Name = "Coordinador";
                roleManager.Create(role3);

                var role4 = new IdentityRole();
                role4.Name = "Radicador";
                roleManager.Create(role4);

                var role5 = new IdentityRole();
                role5.Name = "Abogado";
                roleManager.Create(role5);

                var role6 = new IdentityRole();
                role6.Name = "Adhonorem";
                roleManager.Create(role6);

                var role7 = new IdentityRole();
                role7.Name = "Secretario";
                roleManager.Create(role7);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "bisa";
                user.first_name = "bisa";
                user.last_name = "bisa";
                user.Email = "bisa@bisacorporation.com";
                user.created_date = DateTime.Now;
                user.last_activity = DateTime.Now;
                user.UserEnabled = "1";

                string userPWD = "bisa123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administrador");
                }
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Usuario"))
            {
                var role = new IdentityRole();
                role.Name = "Usuario";
                roleManager.Create(role);
            }
        }
    }
}
