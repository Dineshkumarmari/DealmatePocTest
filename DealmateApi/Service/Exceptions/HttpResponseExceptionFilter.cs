using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DealmateApi.Service.Exceptions;

public class HttpResponseExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BadRequestException badRequestEx)
        {
            context.Result = new ObjectResult(new { error = badRequestEx.Message })
            {
                StatusCode = badRequestEx.StatusCode
            };
            context.ExceptionHandled = true;
        }
        else if (context.Exception is ConflictException conflictEx)
        {
            context.Result = new ObjectResult(new { error = conflictEx.Message })
            {
                StatusCode = conflictEx.StatusCode
            };
            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new ObjectResult(new { error = "An unexpected error occurred." })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
