using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services
{
    public interface IAuthenticator
    {
        Guid UserId { get; }
    }
}
