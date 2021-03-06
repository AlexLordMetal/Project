﻿using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
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
        [Display(Name = "Rating")]
        public int? Rating { get; set; }
    }

    public class OrderViewModelServiceProvider : OrderViewModelCustomer
    {
        public CustomerProfileViewModel Customer { get; set; }
    }

    public class OrderViewModelFeedback
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Feedback")]
        public string Feedback { get; set; }

        [Display(Name = "Rating")]
        public int? Rating { get; set; }

        public CustomerProfileViewModel Customer { get; set; }
    }
}