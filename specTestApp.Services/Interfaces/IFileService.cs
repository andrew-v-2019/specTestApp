using System.Collections.Generic;
using System.Web;
using specTestApp.ViewModels;

namespace specTestApp.Services.Interfaces
{
    public interface IFileService
    {
        FileSaveResult SaveFile(HttpPostedFileBase file, string serverPath);
        void DecorateModelWithFileUrls(List<RequestListItemViewModel> model, string serverFolder);
    }
}
