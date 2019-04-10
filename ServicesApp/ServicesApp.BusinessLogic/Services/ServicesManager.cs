using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ServicesManager : IServicesManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public async Task<List<Service>> GetAllAsync()
        {
            return await context.Services.ToListAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}