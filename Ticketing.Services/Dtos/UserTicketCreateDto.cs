using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Services.Profiles;

namespace Ticketing.Services.Dtos
{
    public class UserTicketCreateDto:ICreateMapper<UserTicket>
    {
        [Required(ErrorMessage ="لطفا آیدی کاربر را وارد کنید")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "لطفا آیدی تیکت را وارد کنید")]

        public int TicketId { get; set; }
    }
}
