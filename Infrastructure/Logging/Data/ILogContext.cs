using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Data
{
    public interface ILogContext : IUnitOfWork
    {
        IRepository<LogEntryData> Entries { get; }
    }
}
