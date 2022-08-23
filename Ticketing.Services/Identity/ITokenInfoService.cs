using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities.Users;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;

namespace Ticketing.Services.Identity
{
    public interface ITokenInfoService
    {
        Task<TokenDto> GenerateToken(User user);
        long GetCurrentUserId();
        TokenValidationParameters GetValidationParameters();
    }
}
