using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using IT_Conference_Service.Helpers.Validation;

namespace IT_Conference_Service.Helpers.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ExceptionsFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var action = context?.ActionDescriptor.DisplayName;
            var exceptionMessage = context.Exception.Message;
            var StatusCode = 500;

            if (context.Exception is ServiceBehaviorException)
            {
                StatusCode = 400;
            }
            else if (context.Exception is DatabaseBehaviorException)
            {
                StatusCode = 404;
            }

            context.Result = new JsonResult(new
            {
                action,
                exceptionMessage
            })
            {
                StatusCode = StatusCode,
            };

            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
