using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.BusinessLogic.IdentityServices;
using Microsoft.AspNet.Identity;
using ServicesApp.ViewModels.ViewModels;
using Microsoft.AspNet.Identity.Owin;

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

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                if (await UserManager.IsInRoleAsync(userId, "Administrator"))
                {
                    return View("IndexAdministrator", services);
                }
                else if (await UserManager.IsInRoleAsync(userId, "ServiceProvider"))
                {
                    return View("IndexServiceProvider", services);
                }
                else if (await UserManager.IsInRoleAsync(userId, "Customer"))
                {
                    return View("IndexCustomer", services);
                }
            }

            return View(services);
        }

        // GET: Approve
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Approve()
        {
            var services = await _serviceManager.GetNotApprovedAsync();
            return View("ApproveAdministrator", services);
        }

        // GET: Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fullServiceViewModel = await _serviceManager.GetByIdAsync<FullServiceViewModel>(id);
            if (fullServiceViewModel == null)
            {
                return HttpNotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                if (await UserManager.IsInRoleAsync(userId, "Administrator"))
                {
                    return View("DetailsAdministrator", fullServiceViewModel);
                }
                else if (await UserManager.IsInRoleAsync(userId, "ServiceProvider"))
                {
                    return View("DetailsServiceProvider", fullServiceViewModel);
                }
                else if (await UserManager.IsInRoleAsync(userId, "Customer"))
                {
                    return View("DetailsCustomer", fullServiceViewModel);
                }
            }

            return View(fullServiceViewModel);
        }

        // GET: Service/Create
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create()
        {
            var createServiceViewModel = new CreateServiceViewModel();
            createServiceViewModel.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");

            return View("CreateAdministrator", createServiceViewModel);
        }

        // POST: Service/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(CreateServiceViewModel createServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.AddAsync(createServiceViewModel);
                return RedirectToAction("Index");
            }

            return View(createServiceViewModel);
        }

        // GET: Service/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var createServiceViewModel = await _serviceManager.GetByIdAsync<CreateServiceViewModel>(id);
            if (createServiceViewModel == null)
            {
                return HttpNotFound();
            }

            createServiceViewModel.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View("EditAdministrator", createServiceViewModel);
        }

        // POST: Service/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(CreateServiceViewModel createServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.ModifyAsync(createServiceViewModel);
                return RedirectToAction("Index");
            }

            return View("EditAdministrator", createServiceViewModel);
        }

        // GET: Service/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fullServiceViewModel = await _serviceManager.GetByIdAsync<FullServiceViewModel>(id);
            if (fullServiceViewModel == null)
            {
                return HttpNotFound();
            }
            return View(fullServiceViewModel);
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
