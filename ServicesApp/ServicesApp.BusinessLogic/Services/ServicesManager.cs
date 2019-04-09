using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.ServicesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ServicesManager : IServicesManager
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public List<Service> GetAll()
        {
            return context.Services.ToList();
        }

        //protected void Dispose()
        //{
        //    context.Dispose();
        //}
    }
}