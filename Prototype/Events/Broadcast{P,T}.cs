using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{   
    public class Broadcast<P, T> 
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
