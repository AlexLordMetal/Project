using System.Collections.Generic;

namespace ServicesApp.DataProvider.DataModels
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Service> Services { get; set; }
    }
}
