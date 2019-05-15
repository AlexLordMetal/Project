using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServicesApp.DataProvider.IdentityModels;
using System.Data.Entity;

namespace ServicesApp.DataProvider
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Add roles to database
            roleManager.Create(new IdentityRole { Name = "Administrator" });
            roleManager.Create(new IdentityRole { Name = "ServiceProvider" });
            roleManager.Create(new IdentityRole { Name = "Customer" });

            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com", PasswordHash = "AEMxJ4C8x1KHdjOkqx9ME3/TVqVdo7BNUEMzGUXQiRxAcPvO90poGqAssBVko0xsig==" };
            var result = userManager.Create(admin);

            if (result.Succeeded)
            {
                // Add role to user
                userManager.AddToRole(admin.Id, "Administrator");
            }

            base.Seed(context);
        }
    }
}