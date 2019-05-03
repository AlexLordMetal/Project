using ServicesApp.DataProvider.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApp.DataProvider.DataModels
{
    public class ServiceProviderService
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ServiceProvider")]
        public string ServiceProviderId { get; set; }
        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual ServiceProviderProfile ServiceProvider { get; set; }

        public int ServicePrice { get; set; }
    }
}