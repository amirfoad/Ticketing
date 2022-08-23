using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Data.Entities;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Dtos;

namespace Ticketing.Services.Repository.Contracts
{
    public interface ITicketService
    {
        Task<OperationResult<List<TicketDto>>> GetForAdmin();
        Task<OperationResult<List<TicketDto>>> GetForUsers(int userId);
        Task<OperationResult<List<PriorityLevelDto>>> GetPriorityLevel();
        Task<OperationResult<TicketDto>> GetTicket(int id);
        Task<OperationResult<CreateResultDto>> Create(TicketCreateDto ticket);
        Task<OperationResult<List<UserTicketDto>>> GetAssignedTickets(int userId);
        Task<OperationResult<bool>> CreateUserTicket(UserTicketCreateDto model);
        Task<OperationResult<bool>> CloseTicket(int id);
        Task<OperationResult<bool>> DeleteTicket(int id, int userCreatedId);
        Task<Ticket> Get(int id);

    }
}
