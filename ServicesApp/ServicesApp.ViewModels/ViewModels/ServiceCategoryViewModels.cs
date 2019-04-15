using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.ViewModels
{
    public class FullServiceCategoryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Category Services")]
        public List<ShortServiceViewModel> Services { get; set; }
    }

    public class ShortServiceCategoryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}