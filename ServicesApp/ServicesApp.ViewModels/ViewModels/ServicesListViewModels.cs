using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServicesListViewModel
    {
        public IEnumerable<ServiceViewModelFull> Services { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
        public string Search { get; set; }
    }
}
