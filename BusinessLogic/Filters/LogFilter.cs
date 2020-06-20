using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Configurations.Filters
{
    public class LogFilter: IActionFilter, IOrderedFilter
    {
        public int Order => -10;

        public readonly ILogger<LogFilter> logger;
        public LogFilter(ILogger<LogFilter> _logger)
        {
            logger = _logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            logger.LogDebug(string.Format("Executed {0} at {1}", actionName, DateTime.UtcNow));
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            logger.LogDebug(string.Format("Executing {0} at {1}", actionName, DateTime.UtcNow));
        }

    }
}
