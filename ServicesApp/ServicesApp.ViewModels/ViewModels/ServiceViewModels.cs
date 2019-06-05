using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
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

        public PhotoViewModel Photo { get; set; }
    }

    public class ServiceViewModelFull : ServiceViewModelShort
    {
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Service Description")]
        public string Description { get; set; }
    }

    public class ServiceViewModelWithRelations : ServiceViewModelFull
    {
        public List<ServiceProviderServiceViewModelCustomer> ServiceProviderServices { get; set; }
    }

    public class ServiceViewModelCreate : ServiceViewModelShort
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000)]
        [Display(Name = "Service Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Service Photo")]
        public HttpPostedFileBase UploadPhoto { get; set; }

        [Display(Name = "Categories")]
        public SelectList ServiceCategories { get; set; }
    }
}