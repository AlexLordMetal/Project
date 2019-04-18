using Microsoft.AspNet.Identity.Owin;
using ServicesApp.BusinessLogic.IdentityServices;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.ViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ServiceController : Controller
    {
        private IServiceManager _serviceManager;
        private IServiceCategoryManager _serviceCategoryManager;
        private ApplicationUserManager _userManager;

        public ServiceController(IServiceManager serviceManager, IServiceCategoryManager serviceCategoryManager)
        {
            _serviceManager = serviceManager;
            _serviceCategoryManager = serviceCategoryManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Service
        public async Task<ActionResult> Index()
        {
            var services = await _serviceManager.GetAllApprovedAsync();
            return View(services);
        }

        // GET: Service/Approve
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Approve()
        {
            var services = await _serviceManager.GetNotApprovedAsync();
            return View(services);
        }

        // GET: Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelFull = await _serviceManager.GetByIdAsync<ServiceViewModelFull>(id);
            if (serviceViewModelFull == null)
            {
                return HttpNotFound();
            }
            return View(serviceViewModelFull);
        }

        // GET: Service/Create
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create()
        {
            var serviceViewModelCreateFull = new ServiceViewModelCreateFull();
            serviceViewModelCreateFull.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");

            return View(serviceViewModelCreateFull);
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(ServiceViewModelCreateShort serviceViewModelCreateShort)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.AddAsync(serviceViewModelCreateShort);
                return RedirectToAction("Index");
            }
            return View(serviceViewModelCreateShort);
        }

        // GET: Service/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelCreateFull = await _serviceManager.GetByIdAsync<ServiceViewModelCreateFull>(id);
            if (serviceViewModelCreateFull == null)
            {
                return HttpNotFound();
            }
            serviceViewModelCreateFull.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(serviceViewModelCreateFull);
        }

        // POST: Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(ServiceViewModelCreateShort serviceViewModelCreateShort)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.ModifyAsync(serviceViewModelCreateShort);
                return RedirectToAction("Index");
            }

            return View(serviceViewModelCreateShort);
        }

        // GET: Service/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelFull = await _serviceManager.GetByIdAsync<ServiceViewModelFull>(id);
            if (serviceViewModelFull == null)
            {
                return HttpNotFound();
            }
            return View(serviceViewModelFull);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _serviceManager.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            base.Dispose(disposing);
        }
    }
}
