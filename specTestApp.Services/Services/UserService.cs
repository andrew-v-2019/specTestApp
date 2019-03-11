using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using specTestApp.Data;
using specTestApp.Services.Interfaces;

namespace specTestApp.Services.Services
{
    public class UserService: IUserService
    {
        public async Task<List<string>> GetManagersEmails()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = new List<string>();
                var role = await context.Roles.FirstOrDefaultAsync(x => x.Name.Equals("Manager"));

                if (role == null)
                {
                    return result;
                }

                var users = context.Users.Where(x => x.Roles.Select(r => r.RoleId).Contains(role.Id));

                var emails = await users.Select(x => x.Email).ToListAsync();

                return emails;

            }
        }
    }
}
