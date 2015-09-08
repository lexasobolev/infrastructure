using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Services
{
    public interface ILogWriter : 
        IHandler<IBroadcast<Before, ILoggable<Before>>>,
        IHandler<IBroadcast<Unhandled, ILoggable<Unhandled>>>,
        IHandler<IBroadcast<Succeeded, ILoggable<Succeeded>>>,
        IHandler<IBroadcast<Failed, ILoggable<Failed>>>
    {
    }
}
