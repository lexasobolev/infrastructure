﻿using System;
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

    class WeakHandler<T> : IWeakHandler<T>
    {
        public WeakHandler(Func<T, Task<bool>> action)
        {
            Action = action;
        }

        Func<T, Task<bool>> Action { get; }

        public Task<bool> HandleAsync(T e)
        {
            return Action(e);
        }
    }
}
