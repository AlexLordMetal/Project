using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServicesApp.DataProvider.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual ServiceProviderProfile ServiceProviderProfile { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}