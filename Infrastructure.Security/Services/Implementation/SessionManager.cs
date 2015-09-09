using Events;
using Infrastructure.Security.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services.Implementation
{
    public class SessionManager : IHandler<SignIn>, IHandler<SignOut>, IHandler<Validation<SignIn>>
    {
        public Task<bool> HandleAsync(Validation<SignIn> e)
        {
            if (string.IsNullOrWhiteSpace(e.Subject.Email) || string.IsNullOrWhiteSpace(e.Subject.Password))
                throw new MissingCredentialsException();

            return Task.FromResult(true);
        }

        public async Task<bool> HandleAsync(SignIn e)
        {
            var userId = await new UserIdLookup(e.Email)
                            .WaitAsync<Guid>();

            var identityUser = await IdentityManagers.UserManager.FindAsync(userId.ToString(), e.Password);
            if (identityUser == null)
                throw new InvalidCredentialsException();

            var identity = await IdentityManagers.UserManager.CreateIdentityAsync(identityUser, DefaultAuthenticationTypes.ApplicationCookie);
            IdentityManagers.AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, identity);

            return true;
        }

        public Task<bool> HandleAsync(SignOut e)
        {
            IdentityManagers.AuthenticationManager.SignOut();
            return Task.FromResult(true);
        }

    }
}
