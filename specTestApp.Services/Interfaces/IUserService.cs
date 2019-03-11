using System.Collections.Generic;
using System.Threading.Tasks;

namespace specTestApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<string>> GetManagersEmails();
    }
}
