using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ServicesApp.BusinessLogic.IdentityServices;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.ViewModels.ViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    [Authorize(Roles = "ServiceProvider")]
    public class ServiceProviderController : Controller
    {
        private ApplicationUserManager _userManager;
        private IServiceProviderManager _serviceProviderManager;

        public ServiceProviderController(IServiceProviderManager serviceProviderManager)
        {
            _serviceProviderManager = serviceProviderManager;
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

        // GET: /ServiceProvider/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.UpdateServiceProviderProfileSuccess ? "Your service provider profile has been updated."
                : message == ManageMessageId.AddServiceRelationSuccess ? "Your service relation has been updated."
                : "";

            var userId = User.Identity.GetUserId();
            var serviceProviderProfileViewModelManage = new ServiceProviderProfileViewModelManage();
            serviceProviderProfileViewModelManage.HasPassword = HasPassword();
            serviceProviderProfileViewModelManage.ServiceProviderProfile = await _serviceProviderManager.GetServiceProviderProfileAsync(userId);
            return View(serviceProviderProfileViewModelManage);
        }

        // GET: /ServiceProvider/UpdateProfile
        public async Task<ActionResult> UpdateProfile()
        {
            var userId = User.Identity.GetUserId();
            var serviceProviderProfileViewModel = await _serviceProviderManager.GetServiceProviderProfileAsync(userId);
            if (serviceProviderProfileViewModel == null)
            {
                return View();
            }
            return View(serviceProviderProfileViewModel);
        }

        // POST: /ServiceProvider/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(ServiceProviderProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                await _serviceProviderManager.UpdateServiceProviderProfileAsync(model, userId);
                return RedirectToAction("Index", new { Message = ManageMessageId.UpdateServiceProviderProfileSuccess });
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.Error });

        }

        // GET: /ServiceProvider/Services
        public async Task<ActionResult> Services()
        {
            return View(await _serviceProviderManager.GetServiceProviderServicesAsync(User.Identity.GetUserId()));
        }

        // GET: /ServiceProvider/AddServiceRelation
        public async Task<ActionResult> AddServiceRelation(int? serviceId)
        {
            if (serviceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            var serviceProviderServiceFullViewModel = await _serviceProviderManager.GetServiceRelationAsync(userId, (int)serviceId);
            if (serviceProviderServiceFullViewModel == null)
            {
                View();
            }
            return View(serviceProviderServiceFullViewModel);
        }

        // POST: /ServiceProvider/AddServiceRelation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddServiceRelation(ServiceProviderServiceRelationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.ServiceProviderId = User.Identity.GetUserId();
            if (model.ServiceProviderId != null)
            {
                await _serviceProviderManager.AddOrUpdateServiceRelationAsync(model);
                return RedirectToAction("Index", new { Message = ManageMessageId.AddServiceRelationSuccess });
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.Error });
        }

        // GET: /ServiceProvider/DeleteServiceRelation/5
        public async Task<ActionResult> DeleteServiceRelation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            var serviceProviderServiceFullViewModel = await _serviceProviderManager.GetServiceRelationAsync(userId, (int)id);
            if (serviceProviderServiceFullViewModel == null)
            {
                return HttpNotFound();
            }
            return View(serviceProviderServiceFullViewModel);
        }

        // POST: /ServiceProvider/DeleteServiceRelation/5
        [HttpPost, ActionName("DeleteServiceRelation")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteServiceRelationConfirmed(int? id)
        {
            var userId = User.Identity.GetUserId();
            await _serviceProviderManager.DeleteServiceRelationAsync(userId, (int)id);
            return RedirectToAction("Services");
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

        #region Helpers

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            UpdateServiceProviderProfileSuccess,
            AddServiceRelationSuccess,
            Error
        }

        #endregion
    }
}