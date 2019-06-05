using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.ViewModels;
using ServicesApp.Website.Messages;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ServiceController : Controller
    {
        private IServiceManager _serviceManager;
        private IServiceCategoryManager _serviceCategoryManager;

        public ServiceController(IServiceManager serviceManager, IServiceCategoryManager serviceCategoryManager)
        {
            _serviceManager = serviceManager;
            _serviceCategoryManager = serviceCategoryManager;
        }

        // GET: Service
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.StatusMessage = message;

            var viewModel = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(viewModel);
        }

        // GET: ServiceTable
        public async Task<ActionResult> ServiceTable(ServicesSearchModel searchModel)
        {
            searchModel.IsApproved = true;
            return PartialView(await _serviceManager.GetListAsync(searchModel));
        }

        // GET: Service/Approve
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Approve(int page = 1)
        {
            var viewModel = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(viewModel);
        }

        // GET: NotApprovedServiceTable
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> NotApprovedServiceTable(ServicesSearchModel searchModel)
        {
            searchModel.IsApproved = false;
            return PartialView("ServiceTable", await _serviceManager.GetListAsync(searchModel));
        }

        // GET: Service/NotApprovedCounter
        [Authorize(Roles = "Administrator")]
        public async Task<int?> NotApprovedCounter()
        {
            var count = await _serviceManager.NotApprovedCount();
            if (count == 0)
            {
                return null;
            }
            return count;
        }

        // GET: Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelWithRelations = await _serviceManager.GetByIdAsync<ServiceViewModelWithRelations>(id);
            if (serviceViewModelWithRelations == null)
            {
                return HttpNotFound();
            }
            return View(serviceViewModelWithRelations);
        }

        // GET: Service/Create
        [Authorize(Roles = "Administrator, ServiceProvider")]
        public async Task<ActionResult> Create()
        {            
            var serviceCategories = await _serviceCategoryManager.GetAllAsync();
            if (serviceCategories.Count == 0)
            {
                return RedirectToAction("Index", new { Message = ManageMessage.NoCategoryError });
            }
            var serviceViewModelCreate = new ServiceViewModelCreate();
            serviceViewModelCreate.ServiceCategories = new SelectList(serviceCategories, "Id", "Name");

            return View(serviceViewModelCreate);
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, ServiceProvider")]
        public async Task<ActionResult> Create(ServiceViewModelCreate serviceViewModelCreate)
        {
            imageFileValidator(serviceViewModelCreate);
            if (ModelState.IsValid)
            {   
                imageFileSaver(serviceViewModelCreate);
                var isAdministrator = User.IsInRole("Administrator");
                await _serviceManager.AddAsync(serviceViewModelCreate, isAdministrator);
                return RedirectToAction("Index");
            }
            serviceViewModelCreate.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(serviceViewModelCreate);
        }

        // GET: Service/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelCreate = await _serviceManager.GetByIdAsync<ServiceViewModelCreate>(id);
            if (serviceViewModelCreate == null)
            {
                return HttpNotFound();
            }
            serviceViewModelCreate.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(serviceViewModelCreate);
        }

        // POST: Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(ServiceViewModelCreate serviceViewModelCreate)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.ModifyAsync(serviceViewModelCreate);
                return RedirectToAction("Index");
            }

            return View(serviceViewModelCreate);
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
            base.Dispose(disposing);
        }

        private void imageFileValidator(ServiceViewModelCreate serviceViewModelCreate)
        {
            var imageTypes = new string[]{
                    "image/bmp",
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            if (serviceViewModelCreate.UploadPhoto.ContentLength == 0)
            {
                ModelState.AddModelError("UploadPhoto", "File cannot be zero size");
            }
            else if (!imageTypes.Contains(serviceViewModelCreate.UploadPhoto.ContentType))
            {
                ModelState.AddModelError("UploadPhoto", "Please choose either a BMP, GIF, JPG or PNG image.");
            }
        }

        private void imageFileSaver(ServiceViewModelCreate viewModel)
        {
            var imageName = DateTime.Now.Ticks.ToString();
            var imageExt = System.IO.Path.GetExtension(viewModel.UploadPhoto.FileName).ToLower();
            var imagePath = Server.MapPath("~/Content/DataImages/");

            viewModel.Photo = new PhotoViewModel();
            viewModel.Photo.Url = imagePath + imageName + imageExt;
            viewModel.UploadPhoto.SaveAs(viewModel.Photo.Url);
        }
    }
}