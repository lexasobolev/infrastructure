using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public static class ImpersonatorClaim
    {
        const string Type = "ImpersonatorId";

        public static Guid GetImpersonatorId(this ClaimsIdentity identity)
        {
            return Guid.Parse(identity.FindFirst(Type)?.Value ?? identity.Name);
        }
        
        public static void AddImpersonatorId(this ClaimsIdentity identity, Guid id)
        {
            identity.AddClaim(new Claim(Type, id.ToString()));
        }
    }
}
