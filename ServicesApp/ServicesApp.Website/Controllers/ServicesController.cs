using ServicesApp.BusinessLogic.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServicesManager _servicesManager;
        public ServicesController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

        //
        // GET: Services
        public async Task<ActionResult> Index()
        {
            var model = await _servicesManager.GetAllAsync();
            return View(model);
        }

        ////
        //// GET: Services/Details/5
        //public async Task<ActionResult> Details(int? id)
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

        //// GET: Services/Create
        //[Authorize(Roles = "ServiceProvider")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Services/Create
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "ServiceProvider")]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Category,Name,Description,Price")] Service service)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Services.Add(service);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(service);
        //}

        //// GET: Services/Edit/5
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
        //    return View(service);
        //}

        //// POST: Services/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Category,Name,Description,Price")] Service service)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(service).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(service);
        //}

        //// GET: Services/Delete/5
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

        //// POST: Services/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
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
