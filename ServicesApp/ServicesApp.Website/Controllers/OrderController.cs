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
using ServicesApp.ViewModels.ViewModels;
using Microsoft.AspNet.Identity;

namespace ServicesApp.Website.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IOrderManager _orderManager;
        private IServiceProviderManager _serviceProviderManager; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public OrderController(IOrderManager orderManager, IServiceProviderManager serviceProviderManager)
        {
            _orderManager = orderManager;
            _serviceProviderManager = serviceProviderManager;
        }

        // GET: Order/Create
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Create(int? serviceProviderServiceId)
        {
            if (serviceProviderServiceId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = new OrderViewModelCustomer();
            viewModel.ServiceProviderService = await _serviceProviderManager.GetServiceRelationAsync<ServiceProviderServiceViewModelCustomer>((int)serviceProviderServiceId);
            viewModel.OrderDate = DateTime.Today.AddDays(1);
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
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.Conflict);
        }


        // GET: Order
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var viewModel = await _orderManager.GetCustomerOrdersAsync<OrderViewModelCustomer>(userId);
            //var orders = db.Orders.Include(o => o.Customer);
            return View(viewModel);
        }

        //// GET: Order/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = await db.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// GET: Order/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = await db.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CustomerId = new SelectList(db.CustomerProfiles, "Id", "Name", order.CustomerId);
        //    return View(order);
        //}

        //// POST: Order/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,IsComplete,Feedback,CustomerId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CustomerId = new SelectList(db.CustomerProfiles, "Id", "Name", order.CustomerId);
        //    return View(order);
        //}

        //// GET: Order/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = await db.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Order order = await db.Orders.FindAsync(id);
        //    db.Orders.Remove(order);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
