using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServicesApp.ViewModels.ViewModels
{
    public class FullServiceViewModel
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

        [Display(Name = "Category")]
        public string Category { get; set; }
    }

    public class ShortServiceViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Service Name")]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
    }

    public class CreateServiceViewModel
    {
        [Display(Name = "Id")]
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

        [Required]
        [Display(Name = "CategoryId")]
        public int CategoryId { get; set; }

        [Display(Name = "Categories")]
        public SelectList ServiceCategories { get; set; }
    }

}