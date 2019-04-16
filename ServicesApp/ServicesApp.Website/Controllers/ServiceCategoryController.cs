﻿using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.ViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServiceCategoryController : Controller
    {
        private IServiceCategoryManager _serviceCategoryManager;

        public ServiceCategoryController(IServiceCategoryManager serviceCategoryManager)
        {
            _serviceCategoryManager = serviceCategoryManager;
        }

        // GET: ServiceCategories
        public async Task<ActionResult> Index()
        {
            return View(await _serviceCategoryManager.GetAllAsync());
        }

        // GET: ServiceCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fullServiceCategoryViewModel = await _serviceCategoryManager.GetByIdAsync(id);
            if (fullServiceCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(fullServiceCategoryViewModel);
        }

        // GET: ServiceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceCategories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShortServiceCategoryViewModel shortServiceCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                await _serviceCategoryManager.AddAsync(shortServiceCategoryViewModel);
                return RedirectToAction("Index");
            }

            return View(shortServiceCategoryViewModel);
        }

        // GET: ServiceCategories/Edit/5                            //Short or Full?????????????????????
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var shortServiceCategoryViewModel = await _serviceCategoryManager.GetShortByIdAsync(id);
            if (shortServiceCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(shortServiceCategoryViewModel);
        }

        // POST: ServiceCategories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShortServiceCategoryViewModel shortServiceCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                await _serviceCategoryManager.ModifyAsync(shortServiceCategoryViewModel);
                return RedirectToAction("Index");
            }
            return View(shortServiceCategoryViewModel);
        }

        // GET: ServiceCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fullServiceCategoryViewModel = await _serviceCategoryManager.GetByIdAsync(id);
            if (fullServiceCategoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(fullServiceCategoryViewModel);
        }

        // POST: ServiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _serviceCategoryManager.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        //Is it necessary?
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
