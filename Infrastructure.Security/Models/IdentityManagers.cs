using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace Infrastructure.Security.Models
{
    public static class IdentityManagers
    {
        public static IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        public static AppUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        public static AppSignInManager SignInManager
        {
            get
            {
                return Request.GetOwinContext().Get<AppSignInManager>();
            }
        }

        static HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
    }
}