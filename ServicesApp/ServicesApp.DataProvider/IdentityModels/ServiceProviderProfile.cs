using ServicesApp.DataProvider.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApp.DataProvider.IdentityModels
{
    public class ServiceProviderProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConfirmDocs { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Service> Services { get; set; }

        public virtual List<ServiceProviderService> ServiceProviderServices { get; set; }
    }
}