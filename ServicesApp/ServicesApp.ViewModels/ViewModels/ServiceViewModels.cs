using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServiceViewModel
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

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

        public PhotoViewModel Photo { get; set; }
    }

    public class ServiceViewModelWithRelations : ServiceViewModel
    {
        public List<ProviderServiceViewModelCustomer> ServiceProviderServices { get; set; }
    }

    public class ServiceViewModelCreate : ServiceViewModel
    {
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Service Photo Id")]
        public int? PhotoId { get; set; }

        [Display(Name = "Service Photo")]
        public HttpPostedFileBase UploadPhoto { get; set; }

        [Display(Name = "Categories")]
        public SelectList ServiceCategories { get; set; }
    }
}