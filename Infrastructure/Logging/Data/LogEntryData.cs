using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.Data
{
    public class LogEntryData
    {
        public int Id { get; private set; }
        public Guid UserId { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Text { get; set; }
        public string Error { get; set; }
        public string Xml { get; set; }
    }
}
