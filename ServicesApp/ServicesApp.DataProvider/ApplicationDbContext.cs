using Microsoft.AspNet.Identity.EntityFramework;
using ServicesApp.DataProvider.IdentityModels;

namespace ServicesApp.DataProvider
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
