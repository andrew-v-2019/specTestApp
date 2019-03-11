using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using specTestApp.Data.Entities;

namespace specTestApp.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var identityRoles = new List<IdentityRole>();
            identityRoles.Add(new IdentityRole { Name = "Manager" });
            identityRoles.Add(new IdentityRole { Name = "Client" });

            foreach (var role in identityRoles)
            {
                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
            };

            var admin = new ApplicationUser();
            userManager.UserLockoutEnabledByDefault = false;
            admin.Email = "admin@admin.com";
            admin.UserName = "admin@admin.com";
            admin.LockoutEnabled = false;
            admin.EmailConfirmed = true;


            var identityResult = userManager.Create(admin, "admin");
            if (identityResult.Succeeded)
            {
                userManager.AddToRole(admin.Id, "Manager");
            }

            base.Seed(context);
        }
    }
}
