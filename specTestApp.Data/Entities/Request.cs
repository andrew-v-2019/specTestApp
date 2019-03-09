
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace specTestApp.Data.Entities
{
    [Table("Request")]
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        public string Caption { get; set; }

        public string Message { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
