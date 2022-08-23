using Microsoft.AspNetCore.Mvc;
using Ticketing.Services.Common.Enums;
using Ticketing.Services.Common.Models;

namespace Ticketing.WebApi.WebFrameWork.Filters
{
    public class ServerErrorResult : IActionResult
    {
        public string Message { get; }

        public ServerErrorResult(string message)
        {
            Message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new ApiResult(false, ApiResultStatusCode.ServerError, Message);

            await context.HttpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
