using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.DataProvider.ServicesModels
{
    public class ServiceTime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Duration { get; set; }
        public bool IsFree { get; set; }
    }
}