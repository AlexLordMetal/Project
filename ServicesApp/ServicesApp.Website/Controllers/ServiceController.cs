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
        
        // GET: Service
        public async Task<ActionResult> Index()
        {
            var services = await _serviceManager.GetAllAsync();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                if (await _userManager.IsInRoleAsync(userId, "Administrator"))
                {
                    return View("IndexAdministrator", services);
                }
                else if (await _userManager.IsInRoleAsync(userId, "ServiceProvider"))
                {
                    return View("IndexServiceProvider", services);
                }
                else if (await _userManager.IsInRoleAsync(userId, "Customer"))
                {
                    return View("IndexCustomer", services);
                }
            }

            return View(services);
        }

        // GET: Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fullServiceViewModel = await _serviceManager.GetByIdAsync(id);
            if (fullServiceViewModel == null)
            {
                return HttpNotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                if (await _userManager.IsInRoleAsync(userId, "Administrator"))
                {
                    return View("DetailsAdministrator", fullServiceViewModel);
                }
                else if (await _userManager.IsInRoleAsync(userId, "ServiceProvider"))
                {
                    return View("DetailsServiceProvider", fullServiceViewModel);
                }
                else if (await _userManager.IsInRoleAsync(userId, "Customer"))
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
            var createServiceViewModel = new CreateServiceViewModel() { ServiceCategories = new List<ShortServiceCategoryViewModel>() };
            createServiceViewModel.ServiceCategories = await _serviceCategoryManager.GetAllAsync();

            return View(createServiceViewModel);
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

        //// GET: Service/Edit/5
        //[Authorize(Roles = "Administrator")]
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Service service = await db.Services.FindAsync(id);
        //    if (service == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryId = new SelectList(db.ServiceCategories, "Id", "Name", service.CategoryId);
        //    return View(service);
        //}

        //// POST: Service/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,CategoryId")] Service service)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(service).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryId = new SelectList(db.ServiceCategories, "Id", "Name", service.CategoryId);
        //    return View(service);
        //}

        //// GET: Service/Delete/5
        //[Authorize(Roles = "Administrator")]
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Service service = await db.Services.FindAsync(id);
        //    if (service == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(service);
        //}

        //// POST: Service/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Service service = await db.Services.FindAsync(id);
        //    db.Services.Remove(service);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

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
