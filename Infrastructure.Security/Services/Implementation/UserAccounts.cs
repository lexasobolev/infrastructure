using Events;
using Infrastructure.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services.Implementation
{
    public class UserAccounts : IHandler<EnsureUserExists>
    {
        public async Task<bool> HandleAsync(EnsureUserExists e)
        {
            var appUser = await IdentityManagers.UserManager.FindByIdAsync(e.UserId.ToString());
            if (appUser == null)
            {
                appUser = new AppUser
                {
                    Id = e.UserId.ToString(),
                    UserName = e.UserId.ToString()
                };

                var result = await IdentityManagers.UserManager.CreateAsync(appUser);
                if (!result.Succeeded)
                    throw new InvalidOperationException(string.Join(", ", result.Errors));

            }
            await e.ReplyAsync(appUser);
            return true;
        }
    }
}
