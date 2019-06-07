using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.ViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServiceCategoryController : Controller
    {
        private IServiceCategoryManager _serviceCategoryManager;

        public ServiceCategoryController(IServiceCategoryManager serviceCategoryManager)
        {
            _serviceCategoryManager = serviceCategoryManager;
        }

        // GET: ServiceCategories
        public async Task<ActionResult> Index()
        {
            return View(await _serviceCategoryManager.GetAllAsync());
        }

        // GET: ServiceCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceCategoryViewModelFull = await _serviceCategoryManager.GetByIdAsync<ServiceCategoryViewModelFull>(id);
            if (serviceCategoryViewModelFull == null)
            {
                return HttpNotFound();
            }
            return View(serviceCategoryViewModelFull);
        }

        // GET: ServiceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServiceCategoryViewModelShort serviceCategoryViewModelShort)
        {
            if (ModelState.IsValid)
            {
                await _serviceCategoryManager.AddAsync(serviceCategoryViewModelShort);
                return RedirectToAction("Index");
            }
            return View(serviceCategoryViewModelShort);
        }

        // GET: ServiceCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceCategoryViewModelShort = await _serviceCategoryManager.GetByIdAsync<ServiceCategoryViewModelShort>(id);
            if (serviceCategoryViewModelShort == null)
            {
                return HttpNotFound();
            }
            return View(serviceCategoryViewModelShort);
        }

        // POST: ServiceCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ServiceCategoryViewModelShort serviceCategoryViewModelShort)
        {
            if (ModelState.IsValid)
            {
                await _serviceCategoryManager.ModifyAsync(serviceCategoryViewModelShort);
                return RedirectToAction("Index");
            }
            return View(serviceCategoryViewModelShort);
        }

        // GET: ServiceCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceCategoryViewModelShort = await _serviceCategoryManager.GetByIdAsync<ServiceCategoryViewModelShort>(id);
            if (serviceCategoryViewModelShort == null)
            {
                return HttpNotFound();
            }
            return View(serviceCategoryViewModelShort);
        }

        // POST: ServiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _serviceCategoryManager.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}