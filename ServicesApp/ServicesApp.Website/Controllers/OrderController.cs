using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.ViewModels.ViewModels;
using ServicesApp.Website.HelpClasses;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IOrderManager _orderManager;
        private IProviderServiceRelationManager _providerServiceRelationManager;
        private ICustomerManager _customerManager;

        public OrderController(IOrderManager orderManager, IProviderServiceRelationManager providerServiceRelationManager, ICustomerManager customerManager)
        {
            _orderManager = orderManager;
            _providerServiceRelationManager = providerServiceRelationManager;
            _customerManager = customerManager;
        }

        // GET: Order/Create
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Create(int? serviceProviderServiceId)
        {
            if (serviceProviderServiceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            if (!await _customerManager.IsCustomerProfileExistAsync(userId))
            {
                return RedirectToAction("Index", "Customer", new { Message = ManageMessage.NullErrorCustomerProfile });
            }
            var viewModel = new OrderViewModelCreate();
            viewModel.ServiceProviderService = await _providerServiceRelationManager.GetServiceRelationAsync<ProviderServiceViewModelCustomer>((int)serviceProviderServiceId);
            await setDates(viewModel);
            return View(viewModel);
        }

        //POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Create(OrderViewModelCustomer viewModel)
        {
            if (ModelState.IsValid)
            {
                await _orderManager.CreateOrderAsync(viewModel, User.Identity.GetUserId());
                return RedirectToAction("Index", "Order", new { Message = ManageMessage.CreateOrderSuccess });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        
        // GET: Order
        [Authorize]
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.StatusMessage = message;

            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Customer"))
            {
                var viewModel = await _orderManager.GetCustomerOrdersAsync<OrderViewModelCustomer>(userId);
                return View("Customer/Index", viewModel);
            }
            if (User.IsInRole("ServiceProvider"))
            {
                var viewModel = await _orderManager.GetServiceProviderOrdersAsync<OrderViewModelServiceProvider>(userId);
                return View("ServiceProvider/Index", viewModel);
            }
            return HttpNotFound();
        }

        // GET: Order/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = await _orderManager.GetOrderByIdAsync<OrderViewModelServiceProvider>((int)id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            if (userId == viewModel.CustomerId && User.IsInRole("Customer"))
            {
                return View("Customer/Details", viewModel);
            }
            if (userId == viewModel.ServiceProviderService.ServiceProviderId && User.IsInRole("ServiceProvider"))
            {
                return View("ServiceProvider/Details", viewModel);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        //GET: Order/Confirm/5
        [Authorize(Roles = "ServiceProvider")]
        public async Task<ActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await _orderManager.ConfirmOrderAsync((int)id, User.Identity.GetUserId());
            return RedirectToAction("Details", new { id });
        }

        // GET: Order/Complete/5
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Complete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = await _orderManager.GetOrderByIdAsync<OrderViewModelServiceProvider>((int)id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == viewModel.CustomerId)
            {
                return View(viewModel);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        // POST: Order/Complete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Complete(OrderViewModelShort viewModel)
        {
            if (ModelState.IsValid && User.Identity.GetUserId() == viewModel.CustomerId)
            {
                viewModel.IsComplete = true;
                await _orderManager.ModifyAsync(viewModel);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Complete", new { id = viewModel.Id });
        }

        // GET: Order/NotConfirmedCounter
        [Authorize(Roles = "ServiceProvider")]
        public async Task<int?> NotConfirmedCounter()
        {
            var count = await _orderManager.NotConfirmedCount(User.Identity.GetUserId());
            if (count == 0)
            {
                return null;
            }
            return count;
        }

        // GET: Order/NotCompletedCounter
        [Authorize(Roles = "Customer")]
        public async Task<int?> NotCompletedCounter()
        {
            var count = await _orderManager.NotCompletedCount(User.Identity.GetUserId());
            if (count == 0)
            {
                return null;
            }
            return count;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private async Task setDates(OrderViewModelCreate viewModel)
        {
            var excludedDates = await _orderManager.GetExcludedDatesAsync(viewModel.ServiceProviderService.ServiceProviderId);
            var orderDate = DateTime.Today.AddDays(1);
            while (excludedDates.Contains(orderDate))
            {
                orderDate = orderDate.AddDays(1);
            }
            viewModel.ExcludedDates = JsonConvert.SerializeObject(excludedDates);
            viewModel.OrderDate = orderDate;
        }
    }
}
