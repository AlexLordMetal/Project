﻿using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ServiceController : Controller
    {
        private IServiceManager _serviceManager;
        private IServiceCategoryManager _serviceCategoryManager;

        public ServiceController(IServiceManager serviceManager, IServiceCategoryManager serviceCategoryManager)
        {
            _serviceManager = serviceManager;
            _serviceCategoryManager = serviceCategoryManager;
        }

        // GET: Service
        public async Task<ActionResult> Index()
        {
            var viewModel = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(viewModel);
        }

        // GET: ServiceTable
        public async Task<ActionResult> ServiceTable(ServicesSearchModel searchModel)
        {
            searchModel.IsApproved = true;
            return PartialView(await _serviceManager.GetListAsync(searchModel));
        }

        // GET: Service/Approve
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Approve(int page = 1)
        {
            var viewModel = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(viewModel);
        }

        // GET: NotApprovedServiceTable
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> NotApprovedServiceTable(ServicesSearchModel searchModel)
        {
            searchModel.IsApproved = false;
            return PartialView("ServiceTable", await _serviceManager.GetListAsync(searchModel));
        }

        // GET: Service/NotApprovedCounter
        [Authorize(Roles = "Administrator")]
        public async Task<int?> NotApprovedCounter()
        {
            var count = await _serviceManager.NotApprovedCount();
            if (count == 0)
            {
                return null;
            }
            return count;
        }

        // GET: Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelWithRelations = await _serviceManager.GetByIdAsync<ServiceViewModelWithRelations>(id);
            if (serviceViewModelWithRelations == null)
            {
                return HttpNotFound();
            }
            return View(serviceViewModelWithRelations);
        }

        // GET: Service/Create
        [Authorize(Roles = "Administrator, ServiceProvider")]
        public async Task<ActionResult> Create()
        {
            var serviceViewModelCreateFull = new ServiceViewModelCreateFull();
            if (serviceViewModelCreateFull.ServiceCategories == null)
            {

            }
            serviceViewModelCreateFull.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");

            return View(serviceViewModelCreateFull);
        }

        // POST: Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, ServiceProvider")]
        public async Task<ActionResult> Create(ServiceViewModelCreateShort serviceViewModelCreateShort)
        {
            if (ModelState.IsValid)
            {
                var isAdministrator = User.IsInRole("Administrator");
                await _serviceManager.AddAsync(serviceViewModelCreateShort, isAdministrator);
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.Conflict); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        // GET: Service/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelCreateFull = await _serviceManager.GetByIdAsync<ServiceViewModelCreateFull>(id);
            if (serviceViewModelCreateFull == null)
            {
                return HttpNotFound();
            }
            serviceViewModelCreateFull.ServiceCategories = new SelectList(await _serviceCategoryManager.GetAllAsync(), "Id", "Name");
            return View(serviceViewModelCreateFull);
        }

        // POST: Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(ServiceViewModelCreateShort serviceViewModelCreateShort)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.ModifyAsync(serviceViewModelCreateShort);
                return RedirectToAction("Index");
            }

            return View(serviceViewModelCreateShort);
        }

        // GET: Service/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var serviceViewModelFull = await _serviceManager.GetByIdAsync<ServiceViewModelFull>(id);
            if (serviceViewModelFull == null)
            {
                return HttpNotFound();
            }
            return View(serviceViewModelFull);
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}