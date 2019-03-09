using System.Web;
using System.IO;
using specTestApp.Web.Models;
using System;
using specTestApp.ViewModels;
using System.Collections.Generic;

namespace specTestApp.Services
{
    public class FileService : IFileService
    {
        private string fileContainer = "Files";

        public FileService()
        {

        }

        private void CreateDirectory(string folderOnServer)
        {
            var di = new DirectoryInfo(folderOnServer);

            if (!di.Exists)
            {
                di.Create();
            }
        }

        public void DecorateModelWithFileUrls(List<RequestListItemViewModel> model)
        {
            foreach (var item in model)
            {
                item.FileUrl = $"{fileContainer}/{item.FileUrl}";
            }
        }

        public FileSaveResult SaveFile(HttpPostedFileBase file, string serverPath)
        {
            var result = new FileSaveResult();
            if (file != null)
            {
                var folderOnServer = serverPath + fileContainer;

                CreateDirectory(folderOnServer);

                var sintethicFileName = GetSintethicFileName(file);

                var fullFilePath = folderOnServer + sintethicFileName;

                file.SaveAs(fullFilePath);
                result.FileName = sintethicFileName;
                result.OrigFileName = file.FileName;
            }
            return result;
        }

        private string GetSintethicFileName(HttpPostedFileBase file)
        {
            var ext = GetExtension(file);
            var sintethicFileName = $"{Guid.NewGuid().ToString()}{ext}";
            return sintethicFileName;
        }


        private string GetExtension(HttpPostedFileBase file)
        {
            string ext = Path.GetExtension(file.FileName);
            return ext;
        }
    }
}
