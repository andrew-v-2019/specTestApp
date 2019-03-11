using System.ComponentModel.DataAnnotations;
using System.Web;

namespace specTestApp.ViewModels
{
    public class CreateRequestViewModel
    {
        [Required]
        [Display(Name = "Тема")]
        [StringLength(50, ErrorMessage = "Заголовок слишком длинный")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Сообщение")]
        [StringLength(5000, ErrorMessage = "Сообщение слишком длинное")]
        public string Message { get; set; }

        public bool DenyCreation { get; set; }
        public string DenyCreationReason { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}