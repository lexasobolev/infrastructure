using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using UI.Web.App_Start;
using Infrastructure;
using Events;

namespace UI.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            new AppStartup()
                .RaiseAsync()
                .Wait();
        }
    }
}