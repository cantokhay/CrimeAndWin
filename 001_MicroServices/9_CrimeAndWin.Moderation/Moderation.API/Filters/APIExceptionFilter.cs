using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Moderation.API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Title = "Beklenmeyen bir hata oluştu",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status500InternalServerError
            };
            context.Result = new ObjectResult(problem) { StatusCode = problem.Status };
            context.ExceptionHandled = true;
        }
    }
}
