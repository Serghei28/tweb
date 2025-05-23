using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace popitka.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Support() => View();
        public ActionResult About() => View();
    }

}