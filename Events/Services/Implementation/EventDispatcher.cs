using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Models;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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
                          select method.InvokeAsync(handler, e);

            var task = Task.WhenAll(matches);
            try
            {
                return (await task).Contains(true);
            }
            catch
            {
                if (task.Exception.InnerExceptions.Count == 1)
                    throw task.Exception.InnerException;
                else
                    throw task.Exception;
            }
        }        
    }

    static class HandlerInvoker
    {
        public static Task<bool> InvokeAsync(this MethodInfo method, object handler, object e)
        {
            try
            {
                return (Task<bool>)method.Invoke(handler, new[] { e });
            }
            catch(Exception ex)
            {
                return Task.FromException<bool>(ex);
            }
        }
    }
}
