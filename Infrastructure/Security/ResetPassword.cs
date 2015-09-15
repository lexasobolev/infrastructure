using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class ResetPassword
    {
        public ResetPassword(Guid userId, string token, string password)
        {
            UserId = userId;
            Token = token;
            Password = password;
        }
        public Guid UserId { get; }
        public string Token { get; }
        public string Password { get; }
    }
}
