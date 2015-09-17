using Events;
using Infrastructure.Security.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Services.Implementation
{
    public class Sessions : IHandler<SignIn>, IHandler<SignInAs>, IHandler<SignOut>, IHandler<Validation<SignIn>>
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

            var appUser = await IdentityManagers.UserManager.FindAsync(userId.ToString(), e.Password);
            if (appUser == null)
                throw new InvalidCredentialsException();

            var identity = await IdentityManagers.UserManager.CreateIdentityAsync(appUser, DefaultAuthenticationTypes.ApplicationCookie);
            IdentityManagers.AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, identity);

            return true;
        }

        public async Task<bool> HandleAsync(SignInAs e)
        {
            var appUser = await IdentityManagers.UserManager.FindByIdAsync(e.UserId.ToString());
            if (appUser == null)
                throw new IdentityNotFoundException();

            var identity = await IdentityManagers.UserManager.CreateIdentityAsync(appUser, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddImpersonatorId(e.ImpersonatorId);
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
