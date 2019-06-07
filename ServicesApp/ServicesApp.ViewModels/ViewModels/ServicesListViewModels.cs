using System.Collections.Generic;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServicesListViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
    }
}
