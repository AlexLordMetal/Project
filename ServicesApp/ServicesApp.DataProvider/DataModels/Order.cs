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
        public OrderTime OrderTime { get; set; }
        public string Feedback { get; set; }

        [ForeignKey("Customer")]
        public string CustomerId { get; set; }

        public virtual CustomerProfile Customer { get; set; }
        public virtual ServiceProviderService ServiceProviderService { get; set; }
    }
}