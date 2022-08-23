using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Services.Profiles;

namespace Ticketing.Services.Dtos
{
    public class UserTicketDto:ICreateMapper<UserTicket>
    {
        public int TicketId { get; set; }
    }
}
