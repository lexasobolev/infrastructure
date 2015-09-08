using Events;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Validation<T>
    {
        public Validation(T subject = default(T))
        {
            Subject = subject;
        }

        public T Subject { get; }
    }
}

namespace Events
{
    public static class ValidationInvocation
    {
        public static Task ValidateAsync<T>(this T e)
        {
            return new Validation<T>(e).ExecuteAsync();
        }
    }
}
