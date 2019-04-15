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
        public string Name { get; set; }
        public string Description { get; set; }
        
        public virtual ServiceCategory Category { get; set; }
    }
}