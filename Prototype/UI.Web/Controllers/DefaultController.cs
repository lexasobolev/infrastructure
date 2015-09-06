using Events;
using Infrastructure;
using Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace UI.Web.Controllers
{
    [RoutePrefix("")]
    public class DefaultController : ApiController
    {
        [HttpGet]
        [Route]
        public async Task<string> Get([FromUri] SignIn signIn)
        {

            //await signIn.ValidateAsync();
            await signIn.RaiseAsync();
            return "Hello World";
        }
    }
}
