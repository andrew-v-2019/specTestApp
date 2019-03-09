using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace specTestApp.Data
{
    public class DBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            identityRoles.Add(new IdentityRole() { Name = "Manager" });
            identityRoles.Add(new IdentityRole() { Name = "Client" });

            foreach (IdentityRole role in identityRoles)
            {
                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
            };

            ApplicationUser admin = new ApplicationUser();
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
