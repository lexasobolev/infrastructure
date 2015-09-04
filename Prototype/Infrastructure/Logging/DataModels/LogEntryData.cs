using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging.DataModels
{
    public class LogEntryData
    {
        public int Id { get; private set; }
        public int UserId { get; set; }
        public DateTime LoggedAt { get; set; }
        public string Text { get; set; }
    }
}
