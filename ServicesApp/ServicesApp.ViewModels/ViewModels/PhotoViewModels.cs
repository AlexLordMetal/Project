using System.ComponentModel.DataAnnotations;

namespace ServicesApp.ViewModels.ViewModels
{
    public class PhotoViewModel
    {
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Display(Name = "Service Photo")]
        public string Url { get; set; }
    }
}