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
    public class ServiceProviderService
    {
        [Key, Column(Order = 0)]
        public int MemberID { get; set; }
        [Key, Column(Order = 1)]
        public int CommentID { get; set; }

        public virtual Service Service { get; set; }
        public virtual ServiceProviderProfile ServiceProvider { get; set; }

        public decimal Price { get; set; }
    }
}
