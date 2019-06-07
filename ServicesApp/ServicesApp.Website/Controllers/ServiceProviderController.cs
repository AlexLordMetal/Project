using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ServicesApp.BusinessLogic.IdentityServices;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.Website.HelpClasses;
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
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.StatusMessage = message;

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
                return RedirectToAction("Index", new { Message = ManageMessage.UpdateProfileSuccess });
            }
            return RedirectToAction("Index", new { Message = ManageMessage.Error });

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

        #endregion
    }
}