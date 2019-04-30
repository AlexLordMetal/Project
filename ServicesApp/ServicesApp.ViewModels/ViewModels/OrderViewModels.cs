using ServicesApp.ViewModels.IdentityViewModels;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        public ServiceProviderServiceFullViewModel ServiceProviderService { get; set; }
    }

    public class OrderCustomerViewModel : OrderViewModelShort
    {
        public ServiceProviderProfileViewModel ServiceProvider { get; set; }
    }

    public class OrderCustomerApproveViewModel : OrderCustomerViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(200)]
        [Display(Name = "Confirmative Documents")]
        public string Feedback { get; set; }
    }

    public class OrderServiceProviderViewModel : OrderViewModelShort
    {
        public CustomerProfileViewModel Customer { get; set; }
    }
}