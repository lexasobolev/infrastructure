using Events;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Web.Services.Implementation
{
    class Test : IHandler<AppStartup>
    {
        public async Task<bool> HandleAsync(AppStartup e)
        {
            throw new NotSupportedException();            
        }
    }

    class Test2 : IHandler<AppStartup>
    {
        public async Task<bool> HandleAsync(AppStartup e)
        {
            throw new InvalidTimeZoneException();            
        }
    }
}
