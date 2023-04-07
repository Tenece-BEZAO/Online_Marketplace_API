using Microsoft.AspNetCore.Mvc.Filters;

namespace Online_Marketplace.Shared.Filters
{
    public abstract class ActionFilterAttribute : Attribute, IAsyncActionFilter, IAsyncResultFilter, IOrderedFilter
    {
        public int Order { get; set; }

        public virtual Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return next();
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
        }

        public virtual Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return next();
        }
    }
}
