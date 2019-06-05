using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Internal()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}