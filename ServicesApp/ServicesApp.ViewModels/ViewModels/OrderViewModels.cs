using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Confirmed by service provider")]
        public bool ServiceProviderConfirm { get; set; }

        [Display(Name = "Completed")]
        public bool IsComplete { get; set; }

        public string CustomerId { get; set; }

        public int ServiceProviderServiceId { get; set; }
    }

    public class OrderViewModelFull : OrderViewModelShort
    {
        public ProviderServiceViewModelCustomer ServiceProviderService { get; set; }
    }

    public class OrderViewModelCreate : OrderViewModelFull
    {
        public string ExcludedDates { get; set; }
    }

    public class OrderViewModelCustomer : OrderViewModelFull
    {
        [DataType(DataType.Text)]
        [StringLength(1000)]
        [Display(Name = "Feedback")]
        public string Feedback { get; set; }

        [Required]
        public int? Rating { get; set; }
    }

    public class OrderViewModelServiceProvider : OrderViewModelCustomer
    {
        public CustomerProfileViewModel Customer { get; set; }
    }

}