using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServiceCategoryViewModelShort
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }

    public class ServiceCategoryViewModelFull : ServiceCategoryViewModelShort
    {
        [Display(Name = "Category Services")]
        public List<ServiceViewModelShort> Services { get; set; }
    }
}