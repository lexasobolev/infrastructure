namespace Infrastructure.Security.Migrations
{
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppIdentityDbContext context)
        {

            var userManager = new AppUserManager(new UserStore<AppUser>(context));

            string louId = "35f62804-f257-4552-9182-0acd1e3d843e";
            var lou = userManager.FindById(louId);
            if (lou == null)
            {
                userManager.Create(new AppUser
                {
                    Id = louId,
                    UserName = louId
                }, "1Password");
            }

        }
    }
}
