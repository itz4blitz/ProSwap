using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProSwap.Data;

[assembly: OwinStartupAttribute(typeof(ProSwap.MVC.Startup))]
namespace ProSwap.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Create Initial Roles and Accounts    
            if (!roleManager.RoleExists("Admin"))
            {

                // Create Admin Role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // Create Admin Account                  

                var user = new ApplicationUser();
                user.UserName = "itz4blitz";
                user.Email = "justinscroggins@outlook.com";

                string userPWD = "P@ssw0rd";

                var chkUser = UserManager.Create(user, userPWD);

                // Add Default User to Admin Role  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // Create Moderator Role     
            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Moderator";
                roleManager.Create(role);

            }

            // Create Registered User Role     
            if (!roleManager.RoleExists("Registered User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Registered User";
                roleManager.Create(role);
            }
        }
    }
}
