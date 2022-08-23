using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ticketing.Services.Common.Enums;
using Ticketing.Services.Common.Models;

namespace Ticketing.WebApi.WebFrameWork.Filters
{
    public class ContentResultFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.Result is ContentResult contentResult)) return;
            var apiResult = new ApiResult(true, ApiResultStatusCode.Success, contentResult.Content);
            context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
        }
    }
}
