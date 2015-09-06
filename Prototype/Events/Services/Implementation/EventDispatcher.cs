using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Models;

namespace Events.Services.Implementation
{
    public class EventDispatcher : IEventDispatcher
    {
        public EventDispatcher(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        IServiceProvider ServiceProvider { get; }

        public async Task<bool> HandleAsync(object e)
        {
            if (e == null)
                return false;

            var handlerTypes = from ct in e.GetType().GetCovariantTypes().Distinct()
                               select ct.GetHandlerType().GetEnumerableType();

            var matches = from handlerType in handlerTypes
                          from handler in (IEnumerable<object>)ServiceProvider.GetService(handlerType)
                          where !(handler is IEventDispatcher)
                          let method = handler.GetType().GetMethod("HandleAsync", new[] { e.GetType() })
                          select (Task<bool>)method.Invoke(handler, new[] { e });

            return (await Task.WhenAll(matches))
                .Contains(true);
        }
    }
}
