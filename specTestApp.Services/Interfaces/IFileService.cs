using specTestApp.ViewModels;
using specTestApp.Web.Models;
using System.Collections.Generic;
using System.Web;

namespace specTestApp.Services
{
    public interface IFileService
    {
        FileSaveResult SaveFile(HttpPostedFileBase file, string serverPath);
        void DecorateModelWithFileUrls(List<RequestListItemViewModel> model);
    }
}
