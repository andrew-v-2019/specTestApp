using specTestApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace specTestApp.Services.Interfaces
{
    public interface IRequestsService
    {
        Task<CreateRequestViewModel> CreateRequestAsync(CreateRequestViewModel model, FileSaveResult file, string currentUserId);
        Task<int?> GetHoursFromLastRequestForUserAsync(string userId);
        Task<List<RequestListItemViewModel>> GetRequestsAsync(FilterViewModel model);
        Task DeactivateRequestAsync(int requestId);
    }
}
