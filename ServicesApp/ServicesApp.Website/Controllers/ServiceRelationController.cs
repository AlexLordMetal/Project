using Microsoft.AspNet.Identity;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.ViewModels;
using ServicesApp.Website.HelpClasses;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ServiceRelationController : Controller
    {

        private IProviderServiceRelationManager _providerServiceRelationManager;
        private IServiceProviderManager _serviceProviderManager;
        private IServiceManager _serviceManager;

        public ServiceRelationController(IProviderServiceRelationManager providerServiceRelationManager, IServiceProviderManager serviceProviderManager, IServiceManager serviceManager)
        {
            _providerServiceRelationManager = providerServiceRelationManager;
            _serviceProviderManager = serviceProviderManager;
            _serviceManager = serviceManager;
        }

        // GET: /ServiceRelation/Index
        [Authorize(Roles = "ServiceProvider")]
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.StatusMessage = message;

            return View(await _providerServiceRelationManager.GetServiceProviderServicesAsync(User.Identity.GetUserId()));
        }

        // GET: /ServiceRelation/Create
        [Authorize(Roles = "ServiceProvider")]
        public async Task<ActionResult> Create(int? serviceId)
        {
            if (serviceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            if (!await _serviceProviderManager.IsServiceProviderProfileExistAsync(userId))
            {
                return RedirectToAction("Index", "ServiceProvider", new { Message = ManageMessage.NullErrorServiceProviderProfile });
            }
            if (await _providerServiceRelationManager.IsRelationExistsAsync(userId, (int)serviceId))
            {
                return RedirectToAction("Index", new { Message = ManageMessage.ServiceRelationExists });
            }
            var viewModel = new ProviderServiceCreateViewModel();
            viewModel.Service = await _serviceManager.GetByIdAsync<ServiceViewModel>((int)serviceId);
            if (viewModel.Service == null)
            {
                return HttpNotFound();
            }
            viewModel.ServiceId = viewModel.Service.Id;
            viewModel.ServiceProviderId = userId;
            return View(viewModel);
        }

        // POST: /ServiceRelation/Create
        [Authorize(Roles = "ServiceProvider")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProviderServiceCreateViewModel viewModel)
        {
            imageFileValidator(viewModel);
            if (ModelState.IsValid)
            {
                imageFileSaver(viewModel);
                await _providerServiceRelationManager.CreateServiceRelationAsync(viewModel);
                return RedirectToAction("Index", new { Message = ManageMessage.AddServiceRelationSuccess });
            }
            viewModel.Service = await _serviceManager.GetByIdAsync<ServiceViewModel>(viewModel.ServiceId);
            return View(viewModel);
        }

        // GET: /ServiceRelation/Delete/5
        [Authorize(Roles = "ServiceProvider")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = await _providerServiceRelationManager.GetServiceRelationAsync<ProviderServiceFullViewModel>((int)id);
            if (viewModel == null || User.Identity.GetUserId() != viewModel.ServiceProviderId)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: /ServiceRelation/Delete/5
        [Authorize(Roles = "ServiceProvider")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            await _providerServiceRelationManager.DeleteServiceRelationAsync((int)id);
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void imageFileValidator(ProviderServiceCreateViewModel viewModel)
        {
            if (viewModel.UploadPhoto != null)
            {
                var imageTypes = new string[]{
                    "image/bmp",
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

                if (viewModel.UploadPhoto.ContentLength == 0)
                {
                    ModelState.AddModelError("UploadPhoto", "File cannot be zero size");
                }
                else if (!imageTypes.Contains(viewModel.UploadPhoto.ContentType))
                {
                    ModelState.AddModelError("UploadPhoto", "Please choose either a BMP, GIF, JPG or PNG image.");
                }
            }
        }

        private void imageFileSaver(ProviderServiceCreateViewModel viewModel)
        {
            if (viewModel.UploadPhoto != null)
            {
                var imageName = DateTime.Now.Ticks.ToString();
                var imageExt = System.IO.Path.GetExtension(viewModel.UploadPhoto.FileName).ToLower();
                var imagePath = "/Content/DataImages/";

                viewModel.Photo = new PhotoViewModel();
                viewModel.Photo.Url = imagePath + imageName + imageExt;
                viewModel.UploadPhoto.SaveAs(Server.MapPath("~") + viewModel.Photo.Url);
            }
        }
    }
}