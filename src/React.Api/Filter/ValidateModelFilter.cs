using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using React.Domain.Common;
using System.Net;

namespace React.Api.Filter
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationMessages = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .SelectMany(kvp =>
                        kvp.Value.Errors.Select(e =>
                            $"{kvp.Key}: {e.ErrorMessage}"
                        )
                    ).ToList();

                var response = new APIBaseResponse<object>
                {
                    ResponseCode = ResponseCodes.VALIDATION_FAILED,
                    ValidationMessage = validationMessages
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No-op
        }
    }

}
