using ServicesApp.ViewModels.IdentityViewModels;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ProviderServiceRelationViewModel
    {
        public int? Id { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceProviderId { get; set; }
        public int? PhotoId { get; set; }
        public PhotoViewModel Photo { get; set; }

        [Required]
        [Display(Name = "Service Price")]
        public int? ServicePrice { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000)]
        [Display(Name = "Provider Description")]
        public string Description { get; set; }
    }

    public class ProviderServiceFullViewModel : ProviderServiceRelationViewModel
    {
        public ServiceViewModel Service { get; set; }
    }

    public class ProviderServiceViewModelCustomer : ProviderServiceFullViewModel
    {
        public ServiceProviderProfileViewModel ServiceProvider { get; set; }
    }

    public class ProviderServiceCreateViewModel : ProviderServiceFullViewModel
    {
        [Display(Name = "Service Photo")]
        public HttpPostedFileBase UploadPhoto { get; set; }
    }

}