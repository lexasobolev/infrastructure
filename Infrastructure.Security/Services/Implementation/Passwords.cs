using Events;
using Infrastructure.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services.Implementation
{
    public class Passwords : IHandler<PasswordCreation>, IHandler<PasswordTokenCreation>, IHandler<ResetPassword>, IHandler<ChangePassword>
    {
        public async Task<bool> HandleAsync(ResetPassword e)
        {
            var result = await IdentityManagers.UserManager.ResetPasswordAsync(e.UserId.ToString(), e.Token, e.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));

            return true;
        }

        public async Task<bool> HandleAsync(ChangePassword e)
        {
            var result = await IdentityManagers.UserManager.ChangePasswordAsync(e.UserId.ToString(), e.CurrentPassword, e.NewPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));

            return true;
        }

        public async Task<bool> HandleAsync(PasswordTokenCreation e)
        {
            await new EnsureUserExists(e.UserId).ExecuteAsync();
            var token = await IdentityManagers.UserManager.GeneratePasswordResetTokenAsync(e.UserId.ToString());
            await e.ReplyAsync(token);
            return true;
        }

        public async Task<bool> HandleAsync(PasswordCreation e)
        {
            var appUser = new AppUser
            {
                Id = e.UserId.ToString(),
                UserName = e.UserId.ToString()
            };

            string password = RandomString.GetNew(8);
            var result = await IdentityManagers.UserManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors));

            await e.ReplyAsync(password);

            return true;
        }
    }
}
