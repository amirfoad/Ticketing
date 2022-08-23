using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;

namespace Ticketing.Data.Entities.Users
{
    public class User : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; } = "";


        #region Navigation Properties

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }

        #endregion

    }
}
