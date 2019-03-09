using System;

namespace specTestApp.ViewModels
{
    public class RequestListItemViewModel
    {
        public int RequestId { get; set; }
        public string Caption { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string OrigFileName { get; set; }
        public string FileUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreationDateString { get; set; }
    }
}
