using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ServicesApp.BusinessLogic.IdentityServices;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.IdentityViewModels;
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
        private ICustomerManager _customerManager;

        public ManageController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
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

        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.UpdateCustomerProfileSuccess ? "Your customer profile has been updated."
                : message == ManageMessageId.UpdateServiceProviderProfileSuccess ? "Your service provider profile has been updated."
                : "";

            var userId = User.Identity.GetUserId();
            if (await UserManager.IsInRoleAsync(userId, "Administrator"))
            {
                return View("ManageAdministrator", HasPassword());
            }
            else if (await UserManager.IsInRoleAsync(userId, "Customer"))
            {
                var customerProfileViewModelManage = new CustomerProfileViewModelManage();
                customerProfileViewModelManage.HasPassword = HasPassword();
                customerProfileViewModelManage.CustomerProfile = await _customerManager.GetCustomerProfileAsync(userId);
                return View("ManageCustomer", customerProfileViewModelManage);
            }
            else if (await UserManager.IsInRoleAsync(userId, "ServiceProvider"))
            {
                return RedirectToAction("Index", "ServiceProvider", new { Message = message });
            }
            return HttpNotFound();
        }

        // GET: /Manage/UpdateCustomerProfile
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> UpdateCustomerProfile()
        {
            var userId = User.Identity.GetUserId();
            var customerProfileViewModel = await _customerManager.GetCustomerProfileAsync(userId);
            if (customerProfileViewModel == null)
            {
                return View();
            }
            return View(customerProfileViewModel);
        }

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
                await _customerManager.UpdateCustomerProfileAsync(model, userId);
                return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.UpdateCustomerProfileSuccess });
            }
            return RedirectToAction("Index", "Manage", new { Message = ManageMessageId.Error });



            //Sign in - does it need?
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

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

        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            if (disposing && _signInManager != null)
            {
                _signInManager.Dispose();
                _signInManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

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
            SetPasswordSuccess,
            RemoveLoginSuccess,
            UpdateCustomerProfileSuccess,
            UpdateServiceProviderProfileSuccess,
            Error
        }

        #endregion
    }
}