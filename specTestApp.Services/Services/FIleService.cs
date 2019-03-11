using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using specTestApp.Services.Interfaces;
using specTestApp.ViewModels;

namespace specTestApp.Services.Services
{
    public class FileService : IFileService
    {
        private const string FileContainer = "Files";

        private static void CreateDirectory(string folderOnServer)
        {
            var di = new DirectoryInfo(folderOnServer);

            if (!di.Exists)
            {
                di.Create();
            }
        }

        public void DecorateModelWithFileUrls(List<RequestListItemViewModel> model, string serverFolder)
        {
            foreach (var item in model)
            {
                var url = serverFolder + FileContainer + '\\' + item.FileUrl;
                item.FileUrl = File.Exists(url) ? $"{FileContainer}/{item.FileUrl}" : string.Empty;
            }
        }

        public FileSaveResult SaveFile(HttpPostedFileBase file, string serverPath)
        {
            var result = new FileSaveResult();
            if (file == null)
            {
                return result;
            }
            var folderOnServer = serverPath + FileContainer;

            CreateDirectory(folderOnServer);

            var sintethicFileName = GetSintethicFileName(file);

            var fullFilePath = folderOnServer + "\\" + sintethicFileName;

            file.SaveAs(fullFilePath);
            result.FileName = sintethicFileName;
            result.OrigFileName = file.FileName;
            return result;
        }

        private static string GetSintethicFileName(HttpPostedFileBase file)
        {
            var ext = GetExtension(file);
            var sintethicFileName = $"{Guid.NewGuid().ToString()}{ext}";
            return sintethicFileName;
        }


        private static string GetExtension(HttpPostedFileBase file)
        {
            var ext = Path.GetExtension(file.FileName);
            return ext;
        }
    }
}
