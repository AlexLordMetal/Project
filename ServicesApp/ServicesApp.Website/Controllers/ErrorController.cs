﻿using System.Web.Mvc;

namespace ServicesApp.Website.Controllers
{
    public class ErrorController : Controller
    {
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