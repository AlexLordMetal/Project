using ServicesApp.DataProvider.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.DataProvider.DataModels
{
    public class ServiceProviderService
    {
        [Key, ForeignKey("ServiceProvider"), Column(Order = 0)]
        public string ServiceProviderId { get; set; }
        [Key, ForeignKey("Service"), Column(Order = 1)]
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; }
        public virtual ServiceProviderProfile ServiceProvider { get; set; }

        public decimal ServicePrice { get; set; }
    }
}