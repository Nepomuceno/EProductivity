using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EProductivity.Web.Controllers
{
    [Authorize]
    [RoutePrefix("")]
    public class HomeController : AsyncController
    {
        [Route("")]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}