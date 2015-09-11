using Events;
using Infrastructure.Security.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services.Implementation
{
    public class OwinConfigurator : IHandler<IAppBuilder>
    {
        public Task<bool> HandleAsync(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppIdentityDbContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);

            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            };

            if (ConfigurationManager.AppSettings["security:CookieName"] != null)
                cookieOptions.CookieName = ConfigurationManager.AppSettings["security:CookieName"];

            if (ConfigurationManager.AppSettings["security:CookieDomain"] != null)
                cookieOptions.CookieDomain = ConfigurationManager.AppSettings["security:CookieDomain"];

            app.UseCookieAuthentication(cookieOptions);

            return Task.FromResult(true);
        }
    }
}
