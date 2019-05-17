using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        public string CustomerId { get; set; }

        public int ServiceProviderServiceId { get; set; }

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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime OrderDate { get; set; }
    }

    public class OrderViewModelCustomer : OrderViewModelShort
    {
        public ServiceProviderServiceViewModelCustomer ServiceProviderService { get; set; }
    }

    public class OrderViewModelServiceProvider : OrderViewModelCustomer
    {
        public CustomerProfileViewModel Customer { get; set; }
    }
}