using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{   
    public interface IBroadcast<P, out T>
    {
        T Subject { get; }
        Exception Exception { get; }
    }

    class Broadcast<P, T> : IBroadcast<P, T>
        where P : Phase
    {
        public Broadcast(T subject, Exception exception = null)
        {
            Subject = subject;
            Exception = exception;
        }

        public T Subject { get; }
        public Exception Exception { get; }
    }
}
