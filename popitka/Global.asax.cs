using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace popitka
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            popitka.UnityConfig.RegisterComponents(); // <--- щрн днаюбэ

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
