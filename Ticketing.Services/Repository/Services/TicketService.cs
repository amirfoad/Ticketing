using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Data.Entities.Users;
using Ticketing.Data.Enums;
using Ticketing.Data.Persistence;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;
using Ticketing.Services.Identity.Manager;
using Ticketing.Services.Repository.Contracts;
using Ticketing.Services.UnitOfWork;
using Ticketing.Services.Utils;

namespace Ticketing.Services.Repository.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork<TicketingDbContext> _unitOfWork;
        private readonly AppUserManager _userManager;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public TicketService(IUnitOfWork<TicketingDbContext> unitOfWork
            , IMapper mapper
            , ICategoryService categoryService
            , AppUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<OperationResult<bool>> CloseTicket(int id)
        {
            var ticket = await Get(id);
            if (ticket is null)
                return OperationResult<bool>.NotFoundResult("تیکت مورد نظر پیدا نشد.");

            ticket.IsClosed = true;
            await _unitOfWork.Commit();
            return OperationResult<bool>.SuccessResult(true);
        }

        public async Task<OperationResult<CreateResultDto>> Create(TicketCreateDto ticket)
        {
            var categoryIsExist = await _categoryService.CategoryIsExist(ticket.CategoryId);
            if (!categoryIsExist)
                return OperationResult<CreateResultDto>.NotFoundResult("دسته بندی مورد نظر پیدا نشد.");

            var ticketToAdd = _mapper.Map<Ticket>(ticket);
            await _unitOfWork.GetRepository<Ticket>().AddAsync(ticketToAdd);
            await _unitOfWork.Commit();
            return OperationResult<CreateResultDto>.SuccessResult(new CreateResultDto { Id = ticketToAdd.Id });
        }

        public async Task<OperationResult<bool>> CreateUserTicket(UserTicketCreateDto model)
        {
            var user = await _unitOfWork.GetRepository<User>()
                .FindAsync(u => u.Id == model.UserId);



            if (user is null)
                return OperationResult<bool>.NotFoundResult("کاربر مورد نظر پیدا نشد.");

            var ticket = await Get(model.TicketId);
            if (ticket is null)
                return OperationResult<bool>.NotFoundResult("تیکت مورد نظر پیدا نشد");




            var IsUserTicketExist = await _unitOfWork.GetRepository<UserTicket>().Query()
                .AnyAsync(u => u.UserId == model.UserId && u.TicketId == model.TicketId);
            if (IsUserTicketExist)
                return OperationResult<bool>.FailureResult("تیکت مورد نظر به کاربر ارجاع داده شده است.");

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("user"))
                return OperationResult<bool>.FailureResult("قابلیت ارجاع دادن برای کاربر مورد نظر امکان ندارد.");


            var userTicketToAdd = _mapper.Map<UserTicket>(model);
            await _unitOfWork.GetRepository<UserTicket>().AddAsync(userTicketToAdd);
            await _unitOfWork.Commit();

            return OperationResult<bool>.SuccessResult(true);
        }

        public async Task<OperationResult<bool>> DeleteTicket(int id, int userCreatedId)
        {
            var ticket = await Get(id);
            if (ticket is null)
                return OperationResult<bool>.NotFoundResult("تیکت مورد نظر پیدا نشد.");
            if (ticket.UserCreatedId != userCreatedId)
                return OperationResult<bool>.FailureResult("شما دسترسی به تیکت مورد نظر ندارید");

            await _unitOfWork.GetRepository<Ticket>().DeleteAsync(ticket);
            await _unitOfWork.Commit();
            return OperationResult<bool>.SuccessResult(true);
        }

        public async Task<Ticket> Get(int id)
        {
            var ticket = await _unitOfWork.GetRepository<Ticket>().FindAsync(t => t.Id == id);
            return ticket;
        }

        public async Task<OperationResult<List<UserTicketDto>>> GetAssignedTickets(int userId)
        {
            var userTickets = await _unitOfWork.GetRepository<UserTicket>().FindAllAsync(u => u.UserId == userId);
            return OperationResult<List<UserTicketDto>>.SuccessResult(_mapper.Map<List<UserTicketDto>>(userTickets));
        }

        public async Task<OperationResult<List<TicketDto>>> GetForAdmin()
        {
            var tickets = await _unitOfWork.GetRepository<Ticket>().GetAllAsync();
            return OperationResult<List<TicketDto>>.SuccessResult(_mapper.Map<List<TicketDto>>(tickets));
        }

        public async Task<OperationResult<List<TicketDto>>> GetForUsers(int userId)
        {
            var tickets = await _unitOfWork.GetRepository<Ticket>()
                 .Query().AsNoTracking().Where(t => t.UserCreatedId == userId).ToListAsync();
            return OperationResult<List<TicketDto>>.SuccessResult(_mapper.Map<List<TicketDto>>(tickets));
        }

        public Task<OperationResult<List<PriorityLevelDto>>> GetPriorityLevel()
        {
            var result = ((PriorityLevel[])Enum.GetValues(typeof(PriorityLevel)))
                .Select(item => new PriorityLevelDto { Priority = item, DisplayName = item.ToDisplay() })
                .ToList();

            return Task.Run(() => OperationResult<List<PriorityLevelDto>>.SuccessResult(result));
        }
        public async Task<OperationResult<TicketDto>> GetTicket(int id)
        {
            var ticket = await Get(id);
            return OperationResult<TicketDto>.SuccessResult(_mapper.Map<TicketDto>(ticket));
        }
    }
}
