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
using System.Xml.Serialization;

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
            await Write("Succeeded", e.Subject);
            return false;
        }

        public async Task<bool> HandleAsync(IBroadcast<Failed, ILoggable<Failed>> e)
        {
            await Write("Failed", e.Subject, e.Exception);
            return false;
        }

        public async Task<bool> HandleAsync(IBroadcast<Unhandled, ILoggable<Unhandled>> e)
        {
            await Write("Unhandled", e.Subject);
            return false;
        }

        public async Task<bool> HandleAsync(IBroadcast<Before, ILoggable<Before>> e)
        {
            await Write("Before", e.Subject);
            return false;
        }

        async Task Write(string phase, object e, Exception ex = null)
        {
            var s = new XmlSerializer(e.GetType());
            var w = new StringWriter();
            s.Serialize(w, e);            

            _context.Entries.Create(new LogEntryData
            {
                LoggedAt = _clock.Time,
                UserId = _authenticator.UserId,
                Text = phase + " " + e,
                Error = ex?.Message,
                Xml = w.ToString()
            });

            await _context.SaveAsync();
        }
    }
}
