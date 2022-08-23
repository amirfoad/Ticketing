using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Services.Dtos;
using Ticketing.Services.Repository.Contracts;
using Ticketing.WebApi.ViewModels;
using Ticketing.WebApi.WebFrameWork;

namespace Ticketing.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Ticket")]

    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ticketService.GetForAdmin();
            return OperationResult(result);

        }
        [HttpGet("[action]")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetMyTickets()
        {

            var result = await _ticketService.GetForUsers(UserId);
            return OperationResult(result);

        }

        [HttpGet("[action]")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetPriorityLevelEnum()
        {

            var result = await _ticketService.GetPriorityLevel();
            return OperationResult(result);

        }
        [HttpGet("[action]")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAssignedTickets()
        {
            var result = await _ticketService.GetAssignedTickets(UserId);
            return OperationResult(result);

        }

        [HttpGet("[action]/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get(int id)
        {
            
            var result = await _ticketService.GetTicket(id);
            return OperationResult(result);

        }

        [HttpPost("[action]")]
        [Authorize(Roles = "admin,user")]

        public async Task<IActionResult> Create(TicketCreateViewModel model)
        {
            var ticketToAdd = new TicketCreateDto
            {
                Title = model.Title,
                Priority = model.Priority,
                Message = model.Message,
                UserCreatedId = UserId,
                CategoryId = model.CategoryId
            };

            var result = await _ticketService.Create(ticketToAdd);
            return OperationResult(result);

        }


        [HttpPost("[action]")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> AssigneTicketToUser(UserTicketCreateDto model)
        {
          
            var result = await _ticketService.CreateUserTicket(model);
            return OperationResult(result);

        }

        [HttpPatch("[action]")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> CloseTicket(int id)
        {

            var result = await _ticketService.CloseTicket(id);
            return OperationResult(result);

        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "admin,user")]

        public async Task<IActionResult> Delete(int id)
        {

            var result = await _ticketService.DeleteTicket(id,UserId);
            return OperationResult(result);

        }



    }
}
