using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticketing.Services.Common.Models;
using Ticketing.Services.Utils;
using Ticketing.WebApi.WebFrameWork.Filters;

namespace Ticketing.WebApi.WebFrameWork
{
    public class BaseController:ControllerBase
    {
        protected int UserId => int.Parse(User.Identity.FindFirstValue("UserId"));


        protected string UserKey => User.FindFirstValue(ClaimTypes.UserData);


        protected IActionResult OperationResult(dynamic result)
        {
            if (result is null)
                return new ServerErrorResult("مشکلی به وجود آمده است");

            if (!((object)result).IsAssignableFromBaseTypeGeneric(typeof(OperationResult<>)))
            {
                throw new InvalidCastException("Given Type is not an OperationResult<T>");
            }


            if (result.IsSuccess) return result.Result is bool ? Ok() : Ok(result.Result);

            if (result.IsNotFound)
                return NotFound(result.ErrorMessage);

            ModelState.AddModelError("GeneralError", result.ErrorMessage);
            return BadRequest(ModelState);

        }
   
    }
}
