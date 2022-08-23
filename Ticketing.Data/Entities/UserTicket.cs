using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;
using Ticketing.Data.Entities.Users;

namespace Ticketing.Data.Entities
{
    public class UserTicket:IEntity
    {
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        #region Navigation Properties

        public User User{ get; set; }
        public Ticket Ticket { get; set; }

        #endregion
    }
}
