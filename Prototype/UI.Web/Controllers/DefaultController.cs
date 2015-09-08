using Events;
using Infrastructure;
using Infrastructure.Logging;
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
        public async Task<IEnumerable<LogEntry>> Get([FromUri] LogQuery query)
        {
            query = query ?? new LogQuery();            
            return await query
                .WaitAsync<IEnumerable<LogEntry>>();            
        }
    }
}
