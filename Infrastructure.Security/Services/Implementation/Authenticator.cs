using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services.Implementation
{
    public class Authenticator : IAuthenticator
    {
        public Guid UserId
        {
            get
            {
                return Guid.Empty;
            }
        }
    }
}
