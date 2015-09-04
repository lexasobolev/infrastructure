using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var handlerType = typeof(IHandler<>).MakeGenericType(e.GetType());
            var handlerTypes = typeof(IEnumerable<>).MakeGenericType(handlerType);
            var handle = handlerType.GetMethod("HandleAsync");
            var handlers = (IEnumerable)ServiceProvider.GetService(handlerTypes);
            var matches = from h in handlers.OfType<object>()
                          where !(h is IEventDispatcher)
                          select (Task<bool>)handle.Invoke(h, new[] { e });

            return (await Task.WhenAll(matches))
                .Contains(true);
        }
    }
}
