using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [DataType(DataType.Text)]
        [StringLength(200)]
        [Display(Name = "Feedback")]
        public string Feedback { get; set; }

        [Display(Name = "Completed")]
        public bool IsComplete { get; set; }

        [Display(Name = "Confirmed by service provider")]
        public bool ServiceProviderConfirm { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
    }

    public class OrderViewModelCustomer : OrderViewModelShort
    {
        public int ServiceProviderServiceId { get; set; }

        public ServiceProviderServiceViewModelCustomer ServiceProviderService { get; set; }
    }
}