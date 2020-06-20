using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configurations.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public readonly ILogger<ExceptionHandlerFilter> logger;
        public ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> _logger)
        {
            logger = _logger;
        }
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            logger.LogError(context.Exception, string.Format("Exception in executing {0} at {1} ", actionName, DateTime.UtcNow));
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            response.WriteAsync(JsonConvert.SerializeObject(
            new ResponseData<string>
            {
                Message = "An error occured.",
                Success = false,
            }));
        }
    }

}
