using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Common;

namespace Ticketing.Data.Entities.Users
{
    public class UserClaim : IdentityUserClaim<int>, IEntity
    {
    }
}
