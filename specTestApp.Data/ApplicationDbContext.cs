using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace specTestApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       

        private void CreateRoles()
        {
            var manager = Roles.Add(new IdentityRole()
            {
                Name = "Manager"
            });
            var cliend = Roles.Add(new IdentityRole()
            {
                Name = "Client"
            });
        }
    }


}
