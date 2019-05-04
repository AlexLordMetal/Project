using ServicesApp.DataProvider.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.DataProvider.DataModels
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public bool ServiceProviderConfirm { get; set; }
        public OrderTime OrderTime { get; set; }
        public string Feedback { get; set; }
        public int Stars { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        [ForeignKey("ServiceProviderService")]
        public string ServiceProviderServiceId { get; set; }

        public virtual CustomerProfile Customer { get; set; }
        public virtual ServiceProviderService ServiceProviderService { get; set; }
    }
}