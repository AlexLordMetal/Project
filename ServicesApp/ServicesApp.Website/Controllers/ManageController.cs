using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ServicesApp.BusinessLogic.IdentityServices;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.BusinessLogic.Services;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IProfilesManager _profilesManager;

        //public ManageController()
        //{
        //}

        public ManageController(IProfilesManager profilesManager)
        {
            _profilesManager = profilesManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.UpdateCustomerProfileSuccess ? "Your customer profile has been updated."
                : message == ManageMessageId.UpdateServiceProviderProfileSuccess ? "Your service provider profile has been updated."
                : "";

            var userId = User.Identity.GetUserId();
            if (await UserManager.IsInRoleAsync(userId, "Administrator"))
            {
                View("ManageAdministrator", HasPassword());
            }
            else if (await UserManager.IsInRoleAsync(userId, "Customer"))
            {
                var manageCustomerProfileViewModel = new ManageCustomerProfileViewModel();
                manageCustomerProfileViewModel.HasPassword = HasPassword();
                var customerProfileViewModel = await _profilesManager.GetCustomerProfileAsync(userId);
                if (customerProfileViewModel == null)
                {
                    customerProfileViewModel = new CustomerProfileViewModel();
                }
                manageCustomerProfileViewModel.CustomerProfileViewModel = customerProfileViewModel;
                return View("ManageCustomer", manageCustomerProfileViewModel);

            }
            else if (await UserManager.IsInRoleAsync(userId, "ServiceProvider"))
            {

            }



            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // GET: /Manage/UpdateCustomerProfile
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> UpdateCustomerProfile()
        {
            var userId = User.Identity.GetUserId();
            var customerProfileViewModel = await _profilesManager.GetCustomerProfileAsync(userId);
            if (customerProfileViewModel == null)
            {
                customerProfileViewModel = new CustomerProfileViewModel();
            }
            return View(customerProfileViewModel);
        }

        //
        // POST: /Manage/UpdateCustomerProfile
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCustomerProfile(CustomerProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                await _profilesManager.UpdateCustomerProfileAsync(model, userId);
            }

            return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.UpdateCustomerProfileSuccess });



            //Sign in - does it need?
        }

        //
        // GET: /Manage/UpdateServiceProviderProfile
        [Authorize(Roles = "ServiceProvider")]
        public async Task<ActionResult> UpdateServiceProviderProfile()
        {
            var userId = User.Identity.GetUserId();
            var serviceProviderProfileViewModel = await _profilesManager.GetServiceProviderProfileAsync(userId);
            if (serviceProviderProfileViewModel == null)
            {
                serviceProviderProfileViewModel = new ServiceProviderProfileViewModel();
            }
            return View(serviceProviderProfileViewModel);
        }

        //
        // POST: /Manage/UpdateServiceProviderProfile
        [HttpPost]
        [Authorize(Roles = "ServiceProvider")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateServiceProviderProfile(ServiceProviderProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                await _profilesManager.UpdateServiceProviderProfileAsync(model, userId);
            }

            return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.UpdateServiceProviderProfileSuccess });



            //Sign in - does it need?
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
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

        #region Вспомогательные приложения

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

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
            RemoveLoginSuccess,
            UpdateCustomerProfileSuccess,
            UpdateServiceProviderProfileSuccess,
            Error
        }

        #endregion
    }
}