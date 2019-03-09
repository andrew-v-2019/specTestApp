using Microsoft.AspNet.Identity.EntityFramework;
using specTestApp.Data.Entities;
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

        public DbSet<Request> Requests { get; set; }

    }


}
