using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class SignInAs
    {
        public SignInAs(Guid userId, Guid impersonatorId)
        {
            UserId = userId;
            ImpersonatorId = impersonatorId;
        }
        public Guid UserId { get; set; }
        public Guid ImpersonatorId { get; set; }
    }
}
