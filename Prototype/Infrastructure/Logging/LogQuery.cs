using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class LogQuery : PaginatedQuery
    {
        // in
        public DateTime LoggedAfter { get; set; }

        // in
        public DateTime LoggedBefore { get; set; }

        // out
        public IEnumerable<LogEntry> Entries { get; set; }
    }

    public class LogEntry
    {
        public int UserId { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Text { get; set; }
    }
}
