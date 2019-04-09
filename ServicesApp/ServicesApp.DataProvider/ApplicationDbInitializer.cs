using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ServicesApp.DataProvider
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // добавляем роли в бд
            roleManager.Create(new IdentityRole { Name = "ServiceProvider" });
            roleManager.Create(new IdentityRole { Name = "Customer" });

            base.Seed(context);
        }
    }
}