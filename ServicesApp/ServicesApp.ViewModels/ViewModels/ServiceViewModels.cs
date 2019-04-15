using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.ViewModels
{
    public class UpdateServiceViewModel
    {
        [Display(Name= "Id")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Service Name")]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000)]
        [Display(Name = "Service Description")]
        public string Description { get; set; }
    }

    public class ShortServiceViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Service Name")]
        public string Name { get; set; }
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