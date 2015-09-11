using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    public static class Event
    {
        public static IDisposable Subscribe<T>(Func<T, Task<bool>> handler, bool weakReference = false)
        {
            if (weakReference)
                return Subscribe(new WeakHandler<T>(handler));
            else
                return Subscribe(new Handler<T>(handler));
        }

        public static IDisposable Subscribe<T>(IWeakHandler<T> handler)
        {
            return new WeakSubscription<T>(handler);
        }

        public static IDisposable Subscribe<T>(IHandler<T> handler)
        {
            return new Subscription<T>(handler);
        }

        public static async Task ExecuteAsync<T>(this T e)
        {
            if (!await RaiseAsync(e))
                throw new NotImplementedException("Required " + typeof(T).Name + " event handler is not registered.");
        }

        [DebuggerHidden]
        public async static Task<bool> RaiseAsync<T>(this T e)
        {
            try
            {
                await Subscription.NotifyAsync(new Broadcast<Before, T>(e));

                if(await Subscription.NotifyAsync(e))
                {
                    await Subscription.NotifyAsync(new Broadcast<Succeeded, T>(e));                    
                    return true;
                }
                else
                {
                    await Subscription.NotifyAsync(new Broadcast<Unhandled, T>(e));                    
                    return false;
                }
            }
            catch(Exception ex)
            {
                await Subscription.NotifyAsync(new Broadcast<Failed, T>(e, ex));
                throw;
            }
        }

        abstract class Subscription : IDisposable
        {
            static ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            static HashSet<Subscription> Instances { get; } = new HashSet<Subscription>();

            protected Subscription()
            {
                Lock.EnterWriteLock();
                try
                {
                    Instances.Add(this);
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }

            public void Dispose()
            {
                Lock.EnterWriteLock();
                try
                {
                    Instances.Remove(this);
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }

            [DebuggerHidden]
            public static async Task<bool> NotifyAsync(object e)
            {                
                Subscription[] instances;
                Lock.EnterReadLock();                
                try
                {
                    instances = Instances.ToArray();                    
                }
                finally
                {
                    Lock.ExitReadLock();
                }

                var matches = Task.WhenAll(from s in instances
                                           select s.NotifyCoreAsync(e));

                return (await matches)
                    .Contains(true);
            }

            protected abstract Task<bool> NotifyCoreAsync(object e);
        }

        class Subscription<T> : Subscription
        {
            readonly IHandler<T> _handler;

            public Subscription(IHandler<T> handler)
            {
                _handler = handler;
            }

            protected override Task<bool> NotifyCoreAsync(object e)
            {
                if (e is T)
                    return _handler.HandleAsync((T)e);
                else
                    return Task.FromResult(false);
            }
        }

        class WeakSubscription<T> : Subscription
        {
            readonly WeakReference<IWeakHandler<T>> _reference;

            public WeakSubscription(IWeakHandler<T> handler)
            {
                _reference = new WeakReference<IWeakHandler<T>>(handler);
            }

            protected override Task<bool> NotifyCoreAsync(object e)
            {
                if (e is T)
                {
                    IWeakHandler<T> handler;
                    if (_reference.TryGetTarget(out handler))
                        return handler.HandleAsync((T)e);
                    
                    Dispose();
                }
                
                return Task.FromResult(false);
            }
        }
    }
}
