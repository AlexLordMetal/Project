using ServicesApp.DataProvider.ServicesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServicesManager
    {
        List<Service> GetAll();
    }
}
