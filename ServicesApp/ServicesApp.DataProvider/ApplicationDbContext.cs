using Microsoft.AspNet.Identity.EntityFramework;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.DataProvider.DataModels;
using System.Data.Entity;

namespace ServicesApp.DataProvider
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<ServiceProviderProfile> ServiceProviderProfiles { get; set; }

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
