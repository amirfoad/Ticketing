using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Data.Entities.Users;
using Ticketing.Data.Persistence;
using Ticketing.Services.Identity.Manager;
using Ticketing.Services.UnitOfWork;

namespace Ticketing.Services.Identity.SeedDatabaseService
{
    public interface ISeedDataBase
    {
        Task SeedAsync();
    }
    public class SeedDataBase : ISeedDataBase
    {

        private readonly AppUserManager _userManager;
        private readonly AppRoleManager _roleManager;
        private readonly IUnitOfWork<TicketingDbContext> _unitOfWork;


        public SeedDataBase(AppUserManager userManager
            , AppRoleManager roleManager
            , IUnitOfWork<TicketingDbContext> unitOfWork
            , TicketingDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;

        }
        public async Task SeedAsync()
        {
            if (!_roleManager.Roles.AsNoTracking().Any(r => r.Name.Equals("user")))
            {
                var role = new Role
                {
                    Name = "user",
                };
                await _roleManager.CreateAsync(role);
            }
            if (!_roleManager.Roles.AsNoTracking().Any(r => r.Name.Equals("admin")))
            {
                var role = new Role
                {
                    Name = "admin",
                };
                await _roleManager.CreateAsync(role);
            }

            if (!_unitOfWork.GetRepository<Category>().Query().AsNoTracking().Any())
            {
                List<Category> categoriesToAdd = new List<Category>()
                {
                    new Category
                    {
                        Title = "برنامه نویسی"
                    },
                       new Category
                    {
                        Title = "شبکه"
                    },
                            new Category
                    {
                        Title = "ویندوز"
                    },
                };
                await _unitOfWork.GetRepository<Category>().AddRangeAsync(categoriesToAdd);
            }
            await _unitOfWork.Commit();

            if (!_userManager.Users.AsNoTracking().Any(u => u.UserName.Equals("admin")))
            {
                var administrator = new User
                { UserName = "administrator@localhost", Email = "administrator@localhost", FirstName = "Admin",LastName="Adminian" };
                await _userManager.CreateAsync(administrator, "Administrator1!");
                await _userManager.AddToRolesAsync(administrator, new[] { "admin" });

            }
        }
    }
}
