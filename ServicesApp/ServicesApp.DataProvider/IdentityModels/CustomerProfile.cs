using ServicesApp.DataProvider.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApp.DataProvider.IdentityModels
{
    public class CustomerProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}