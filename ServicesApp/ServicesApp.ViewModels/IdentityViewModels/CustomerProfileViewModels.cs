using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.IdentityViewModels
{
    public class CustomerProfileViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
    }

    public class ManageCustomerProfileViewModel
    {
        [Display(Name = "Password")]
        public bool HasPassword { get; set; }

        public CustomerProfileViewModel CustomerProfileViewModel { get; set; }
    }

    //public class ManageCustomerProfileViewModel
    //{
    //    [Display(Name = "Password")]
    //    public bool HasPassword { get; set; }

    //    [Display(Name = "Name")]
    //    public string Name { get; set; }

    //    [Display(Name = "Surname")]
    //    public string Surname { get; set; }

    //    [Display(Name = "Phone Number")]
    //    public string Phone { get; set; }
    //}
}
