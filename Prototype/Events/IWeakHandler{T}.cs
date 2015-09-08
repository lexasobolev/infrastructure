using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public interface IWeakHandler<T>
    {
        Task<bool> HandleAsync(T e);
    }
}
