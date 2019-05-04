using ServicesApp.ViewModels.IdentityViewModels;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200)]
        [Display(Name = "Feedback")]
        public string Feedback { get; set; }

        [Display(Name = "Completed")]
        public bool IsComplete { get; set; }

        [Display(Name = "Confirmed by service provider")]
        public bool ServiceProviderConfirm { get; set; }

        [Required]
        [Display(Name = "Order Time")]
        public OrderTimeViewModel OrderTime { get; set; }
    }

    public class OrderViewModelFull : OrderViewModelShort
    {
        public ServiceViewModelFull Service { get; set; }

        public ServiceProviderServiceFullViewModel ServiceProviderService { get; set; }
    }


    public class OrderCustomerViewModel : OrderViewModelShort
    {
        public ServiceProviderProfileViewModel ServiceProvider { get; set; }
    }

    public class OrderServiceProviderViewModel : OrderViewModelShort
    {
        public CustomerProfileViewModel Customer { get; set; }
    }
}