using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;
using Infrastructure.Security.Services;
using System.Diagnostics.Contracts;
using System.IO;
using Infrastructure.Services;

namespace Infrastructure.Logging.Services.Implementation
{
    class LogWriter : ILogWriter
    {
        readonly IAuthenticator _authenticator;
        readonly IClock _clock;

        public LogWriter(IAuthenticator authenticator, IClock clock)
        {
            Contract.Requires<ArgumentNullException>(authenticator != null);
            Contract.Requires<ArgumentNullException>(clock != null);
            _authenticator = authenticator;
            _clock = clock;
        }

        public async Task<bool> HandleAsync(Broadcast<Succeeded, ILoggable<Succeeded>> e)
        {
            await Write("Succeeded", e.Subject.ToString());
            return false;
        }

        public async Task<bool> HandleAsync(Broadcast<Failed, ILoggable<Failed>> e)
        {
            await Write("Failed", e.Subject.ToString(), e.Exception.ToString());
            return false;
        }

        public async Task<bool> HandleAsync(Broadcast<Unhandled, ILoggable<Unhandled>> e)
        {
            await Write("Unhandled", e.Subject.ToString());
            return false;
        }

        public async Task<bool> HandleAsync(Broadcast<Preparing, ILoggable<Preparing>> e)
        {
            await Write("Preparing", e.Subject.ToString());
            return false;
        }

        async Task Write(string phase, string text, string error = "")
        {
            File.AppendAllText("c:\\log.txt",
                _clock.Time.ToString() + "\r\n" +
                _authenticator.UserId.ToString() + "\r\n" +
                phase + "\r\n" +
                text + "\r\n" +
                error + "\r\n\r\n");
        }
    }
}
