using ServicesApp.DataProvider.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.DataProvider.DataModels
{
    public class Service
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual IEnumerable<Photo> Photos { get; set; }
        public virtual IEnumerable<ServiceTime> ServiceTimes { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}