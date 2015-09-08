using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    interface IReply<out TOriginal, out TSubject>
    {
        TOriginal Original { get; }
        TSubject Subject { get; }
    }

    class Reply<TOriginal, TSubject> : IReply<TOriginal, TSubject>
    {
        public Reply(TOriginal original, TSubject subject)
        {
            Original = original;
            Subject = subject;
        }

        public TOriginal Original { get; }
        public TSubject Subject { get; }
    }

    public static class ReplyHelper
    {
        public async static Task<bool> ReplyAsync<TOriginal, TSubject>(this TOriginal original, TSubject subject)
        {
            return await new Reply<TOriginal, TSubject>(original, subject)
                .RaiseAsync();
        }

        public static async Task<T> WaitAsync<T>(this object original)
        {
            T result = default(T);
            var replyed = false;
            await original.ExecuteAsync(
                async (T e) =>
                {
                    result = e;
                    replyed = true;
                    return true;
                });

            if (!replyed)
                throw new NotImplementedException();

            return result;
        }

        public static async Task ExecuteAsync<TOriginal, T1>(this TOriginal original, 
            Func<T1, Task<bool>> h1)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
                await original.ExecuteAsync();
        }
        
        public static async Task ExecuteAsync<TOriginal, T1, T2>(this TOriginal original, 
            Func<T1, Task<bool>> h1, 
            Func<T2, Task<bool>> h2)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T2>(original, h2)))
                await original.ExecuteAsync();
        }

        public static async Task ExecuteAsync<TOriginal, T1, T2, T3>(this TOriginal original, 
            Func<T1, Task<bool>> h1, 
            Func<T2, Task<bool>> h2, 
            Func<T3, Task<bool>> h3)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T2>(original, h2)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T3>(original, h3)))
                await original.ExecuteAsync();
        }

        public static async Task ExecuteAsync<TOriginal, T1, T2, T3, T4>(this TOriginal original, 
            Func<T1, Task<bool>> h1, 
            Func<T2, Task<bool>> h2, 
            Func<T3, Task<bool>> h3, 
            Func<T4, Task<bool>> h4)
        {
            using (Event.Subscribe(new ReplayFilter<TOriginal, T1>(original, h1)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T2>(original, h2)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T3>(original, h3)))
            using (Event.Subscribe(new ReplayFilter<TOriginal, T4>(original, h4)))
                await original.ExecuteAsync();
        }
    }

    class ReplayFilter<TOriginal, TSubject> : IHandler<IReply<TOriginal, TSubject>>
    {
        public ReplayFilter(TOriginal original, Func<TSubject, Task<bool>> handler)
        {
            Original = original;
            Handler = handler;
        }

        TOriginal Original { get; }
        Func<TSubject, Task<bool>> Handler { get; }

        public async Task<bool> HandleAsync(IReply<TOriginal, TSubject> e)
        {
            if (e.Original.Equals(Original))
                return await Handler(e.Subject);

            return false;
        }
    }
}
