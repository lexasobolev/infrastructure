using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class ChangePassword
    {
        public ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        public Guid UserId { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }
    }
}
