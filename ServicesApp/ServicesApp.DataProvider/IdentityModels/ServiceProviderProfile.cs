using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesApp.DataProvider.IdentityModels
{
    public class ServiceProviderProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConfirmDoc { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}