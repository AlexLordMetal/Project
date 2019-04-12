using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.IdentityViewModels
{
    public class ServiceProviderProfileViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Organization Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200)]
        [Display(Name = "Confirmative Documents")]
        public string ConfirmDocs { get; set; }
    }

    public class ManageServiceProviderProfileViewModel
    {
        [Display(Name = "Password")]
        public bool HasPassword { get; set; }

        public ServiceProviderProfileViewModel ServiceProviderProfileViewModel { get; set; }
    }

    //public class ManageServiceProviderProfileViewModel
    //{
    //    [Display(Name = "Password")]
    //    public bool HasPassword { get; set; }

    //    [Display(Name = "Organization Name")]
    //    public string Name { get; set; }

    //    [Display(Name = "Confirmative Documents")]
    //    public string ConfirmDocs { get; set; }
    //}

}