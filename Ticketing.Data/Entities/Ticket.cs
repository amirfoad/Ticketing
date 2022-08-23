using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;
using Ticketing.Data.Entities.Users;
using Ticketing.Data.Enums;

namespace Ticketing.Data.Entities
{
    public class Ticket: BaseAuditableEntity
    {

        public string Title { get; set; }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;
        public string Message { get; set; }

        public bool IsClosed { get; set; } = false;



        #region Foregin Keys

        public int UserCreatedId { get; set; }
        public int CategoryId { get; set; }


        #endregion

        #region Navigation Properties
        public Category Category { get; set; }

        public User User { get; set; }

        public ICollection<UserTicket> UserTickets { get; set; }


        #endregion

    }
}
