using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Data.Enums;
using Ticketing.Services.Profiles;

namespace Ticketing.Services.Dtos
{
    public class TicketDto:ICreateMapper<Ticket>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PriorityLevel Priority { get; set; } 
        public string Message { get; set; }

   
    }
}
