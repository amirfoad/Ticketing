using System.ComponentModel.DataAnnotations;
using Ticketing.Data.Enums;

namespace Ticketing.WebApi.ViewModels
{
    public class TicketCreateViewModel
    {
        [Required(ErrorMessage ="لطفا عنوان را وارد کنید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا اولویت را وارد کنید")]

        public PriorityLevel Priority { get; set; }
        [Required(ErrorMessage = "لطفا پیام را وارد کنید")]

        public string Message { get; set; }

        [Required(ErrorMessage = "لطفا دسته بندی را وارد کنید")]

        public int CategoryId { get; set; }
    }
}
