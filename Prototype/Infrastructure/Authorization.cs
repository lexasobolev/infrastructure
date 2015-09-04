using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Authorization<T>
    {
        public Authorization(T subject = default(T))
        {
            Subject = subject;
        }

        public T Subject { get; }
    }
}

namespace Events
{ 
    public static class AuthorizationInvocation
    {
        public static Task AuthorizeAsync<T>(this T e)
        {
            return new Authorization<T>(e).ExecuteAsync();
        }
    }
}
