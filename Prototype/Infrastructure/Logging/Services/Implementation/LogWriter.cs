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
using Infrastructure.Logging.Data;

namespace Infrastructure.Logging.Services.Implementation
{
    public class LogWriter : ILogWriter
    {
        readonly ILogContext _context;
        readonly IAuthenticator _authenticator;
        readonly IClock _clock;

        public LogWriter(ILogContext context, IAuthenticator authenticator, IClock clock)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Requires<ArgumentNullException>(authenticator != null);
            Contract.Requires<ArgumentNullException>(clock != null);

            _context = context;
            _authenticator = authenticator;
            _clock = clock;
        }

        public async Task<bool> HandleAsync(IBroadcast<Succeeded, ILoggable<Succeeded>> e)
        {
            await Write("Succeeded", e.Subject.ToString());
            return false;
        }

        public async Task<bool> HandleAsync(IBroadcast<Failed, ILoggable<Failed>> e)
        {
            await Write("Failed", e.Subject.ToString(), e.Exception.ToString());
            return false;
        }

        public async Task<bool> HandleAsync(IBroadcast<Unhandled, ILoggable<Unhandled>> e)
        {
            await Write("Unhandled", e.Subject.ToString());
            return false;
        }

        public async Task<bool> HandleAsync(IBroadcast<Before, ILoggable<Before>> e)
        {
            await Write("Preparing", e.Subject.ToString());
            return false;
        }

        async Task Write(string phase, string text, string error = "")
        {
            _context.Entries.Create(new LogEntryData
            {
                LoggedAt = _clock.Time,
                UserId = _authenticator.UserId,
                Text = 
                    phase + "\r\n" +
                    text + "\r\n" +
                    error + "\r\n\r\n"
            });

            await _context.SaveAsync();
        }
    }
}
