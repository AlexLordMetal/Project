using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels.ViewModels
{
    public class ServicesSearchModel
    {
        public bool IsApproved { get; set; }
        public string Search { get; set; }
        public int? CategoryId { get; set; }
        public string OrderBy { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
