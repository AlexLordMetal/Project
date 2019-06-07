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
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private ApplicationUserManager _userManager;
        private ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
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
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.StatusMessage = message;

            var userId = User.Identity.GetUserId();
            var customerProfileViewModelManage = new CustomerProfileViewModelManage();
            customerProfileViewModelManage.HasPassword = HasPassword();
            customerProfileViewModelManage.CustomerProfile = await _customerManager.GetCustomerProfileAsync(userId);
            return View(customerProfileViewModelManage);
        }

        // GET: /Manage/UpdateProfile
        public async Task<ActionResult> UpdateProfile()
        {
            var userId = User.Identity.GetUserId();
            var customerProfileViewModel = await _customerManager.GetCustomerProfileAsync(userId);
            if (customerProfileViewModel == null)
            {
                return View();
            }
            return View(customerProfileViewModel);
        }

        // POST: /Manage/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(CustomerProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                await _customerManager.UpdateCustomerProfileAsync(model, userId);
                return RedirectToAction("Index", new { Message = ManageMessage.UpdateProfileSuccess });
            }
            return RedirectToAction("Index", new { Message = ManageMessage.Error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Manager()
        {
            var userId = User.Identity.GetUserId();
            var customerProfileViewModelManage = new CustomerProfileViewModelManage();
            customerProfileViewModelManage.HasPassword = HasPassword();
            customerProfileViewModelManage.CustomerProfile = await _customerManager.GetCustomerProfileAsync(userId);
            return View(customerProfileViewModelManage);
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