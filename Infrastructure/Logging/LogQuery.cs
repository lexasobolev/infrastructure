using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class LogQuery : PaginatedQuery
    {
        public LogQuery()
        {
            LoggedAfter = DateTime.MinValue;
            LoggedBefore = DateTime.MaxValue;
        }

        // in
        public DateTime LoggedAfter { get; set; }

        // in
        public DateTime LoggedBefore { get; set; }
    }

    public class LogEntry
    {
        public int UserId { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Text { get; set; }
        public string Error { get; set; }
        public string Xml { get; set; }
        
    }
}
