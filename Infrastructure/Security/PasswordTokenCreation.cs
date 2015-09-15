using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class PasswordTokenCreation
    {
        public PasswordTokenCreation(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; }
    }
}
