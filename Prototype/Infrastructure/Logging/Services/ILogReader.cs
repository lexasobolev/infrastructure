using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Services
{
    public interface ILogReader : IHandler<LogQuery>
    {
    }
}
