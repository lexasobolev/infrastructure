using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementation
{
    public class SystemClock : IClock
    {
        public DateTime Time
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
