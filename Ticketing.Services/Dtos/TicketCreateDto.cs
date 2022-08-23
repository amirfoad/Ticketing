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
    public class TicketCreateDto : ICreateMapper<Ticket>
    {
        public string Title { get; set; }
        public PriorityLevel Priority { get; set; }
        public string Message { get; set; }
        public int UserCreatedId { get; set; }

        public int CategoryId { get; set; }
    }
}
