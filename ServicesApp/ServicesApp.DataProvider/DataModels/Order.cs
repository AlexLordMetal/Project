﻿using ServicesApp.DataProvider.IdentityModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApp.DataProvider.DataModels
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public bool ServiceProviderConfirm { get; set; }
        public DateTime OrderDate { get; set; }
        public string Feedback { get; set; }
        public int? Rating { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        [ForeignKey("ServiceProviderService")]
        public int ServiceProviderServiceId { get; set; }

        public virtual CustomerProfile Customer { get; set; }
        public virtual ServiceProviderService ServiceProviderService { get; set; }
    }
}