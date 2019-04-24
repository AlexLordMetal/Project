using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServiceViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Service Name")]
        public string Name { get; set; }
    }

    public class ServiceViewModelFull : ServiceViewModelShort
    {
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Service Description")]
        public string Description { get; set; }
    }
       
    public class ServiceViewModelCreateShort : ServiceViewModelShort
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000)]
        [Display(Name = "Service Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }

    public class ServiceViewModelCreateFull : ServiceViewModelCreateShort
    {
        [Display(Name = "Categories")]
        public SelectList ServiceCategories { get; set; }
    }
}