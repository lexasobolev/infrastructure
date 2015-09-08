using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class SignIn : ILoggable<Before>, ILoggable<Succeeded>, ILoggable<Failed>
    {
        // in
        public string Id { get; set; }

        // in
        public string Password { get; set; }
    }
}
