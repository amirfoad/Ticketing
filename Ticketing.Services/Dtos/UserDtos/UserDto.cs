using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities.Users;
using Ticketing.Services.Profiles;

namespace Ticketing.Services.Dtos.UserDtos
{
    public class UserDto : ICreateMapper<User>
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Roles { get; set; }

    }
    public class UserListDto : ICreateMapper<User>
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
