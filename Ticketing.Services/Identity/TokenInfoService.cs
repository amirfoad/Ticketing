using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities.Users;
using Ticketing.Data.Persistence;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;

namespace Ticketing.Services.Identity
{
    public class TokenInfoService : ITokenInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly TicketingDbContext _db;
        public TokenInfoService(TicketingDbContext db
            , IHttpContextAccessor httpContextAccessor
            , UserManager<User> userManager
            , IConfiguration configuration
            , IMapper mapper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _mapper = mapper;
            _db = db;
        }

        public long GetCurrentUserId()
        {
            return long.Parse(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
        }


        public async Task<TokenDto> GenerateToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var splitedUserRoles = userRoles.Aggregate((s, p) => s + "," + p);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,splitedUserRoles),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };




            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Authentication:JwtExpireMins"]));

            var token = new JwtSecurityToken(
                _configuration["Authentication:JwtIssuer"],
                _configuration["Authentication:JwtAudience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user).ConfigureAwait(false);

            var refTokenDto = _mapper.Map<TokenDto>(user);

            refTokenDto.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
           
            refTokenDto.Roles = splitedUserRoles;
            refTokenDto.UserId = user.Id;

            return refTokenDto;
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Authentication:JwtIssuer"],

                ValidateAudience = true,
                ValidAudience = _configuration["Authentication:JwtAudience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtKey"])),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

    }
}
