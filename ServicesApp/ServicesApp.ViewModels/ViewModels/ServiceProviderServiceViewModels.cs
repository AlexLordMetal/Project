using ServicesApp.ViewModels.IdentityViewModels;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServiceProviderServiceRelationViewModel
    {
        public int? Id { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceProviderId { get; set; }

        [Required]
        [Display(Name = "Service Price")]
        public int ServicePrice { get; set; }
    }

    public class ServiceProviderServiceFullViewModel : ServiceProviderServiceRelationViewModel
    {
        public ServiceViewModelFull Service { get; set; }
    }

    public class ServiceProviderServiceViewModelCustomer : ServiceProviderServiceRelationViewModel
    {
        public ServiceProviderProfileViewModel ServiceProvider { get; set; }
    }
}