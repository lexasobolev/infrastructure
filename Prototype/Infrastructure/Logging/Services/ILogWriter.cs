using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Services
{
    public interface ILogWriter : 
        IHandler<Broadcast<Preparing, ILoggable<Preparing>>>,
        IHandler<Broadcast<Unhandled, ILoggable<Unhandled>>>,
        IHandler<Broadcast<Succeeded, ILoggable<Succeeded>>>,
        IHandler<Broadcast<Failed, ILoggable<Failed>>>
    {
    }
}
