using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.ViewModels
{
    public class OrderTimeViewModel
    {
        [Required]
        [Display(Name = "Order Date")]
        public DateTime Date { get; set; }
    }
}
