using System;
using System.Net;
using Estacionamiento.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Estacionamiento.Api.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class AppExceptionFilterAttribute : ExceptionFilterAttribute
    {
         private readonly ILogger<Exception> _Logger;

        public AppExceptionFilterAttribute(ILogger<Exception> logger)
        {
            _Logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context != null)
            {
                context.HttpContext.Response.StatusCode = context.Exception is AppException 
                    ? ((int)HttpStatusCode.BadRequest) 
                    : ((int)HttpStatusCode.InternalServerError);

                _Logger.LogError(context.Exception, context.Exception.Message, new[] { context.Exception.StackTrace });

                var msg = new
                {
                    context.Exception.Message                    
                };

                context.Result = new ObjectResult(msg);
            }
        }
    }
}
