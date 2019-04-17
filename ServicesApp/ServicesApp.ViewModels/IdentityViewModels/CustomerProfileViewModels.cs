using System.ComponentModel.DataAnnotations;

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

    public class CustomerProfileViewModelManage
    {
        [Display(Name = "Password")]
        public bool HasPassword { get; set; }

        public CustomerProfileViewModel CustomerProfile { get; set; }
    }

}
