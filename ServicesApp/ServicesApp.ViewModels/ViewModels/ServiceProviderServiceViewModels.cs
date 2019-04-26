using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServiceProviderServiceRelationDeleteViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceProviderId { get; set; }
    }

    public class ServiceProviderServiceRelationViewModel : ServiceProviderServiceRelationDeleteViewModel
    {
        [Required]
        //[DataType(DataType.Currency)]
        [Display(Name = "Service Price")]
        public decimal ServicePrice { get; set; }
    }

    public class ServiceProviderServiceFullViewModel : ServiceProviderServiceRelationViewModel
    {
        public ServiceViewModelFull Service { get; set; }
    }
}
