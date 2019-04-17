using System.ComponentModel.DataAnnotations;

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

    public class ServiceProviderProfileViewModelManage
    {
        [Display(Name = "Password")]
        public bool HasPassword { get; set; }

        public ServiceProviderProfileViewModel ServiceProviderProfile { get; set; }
    }

}