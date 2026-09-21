using Microsoft.AspNetCore.Mvc.Filters;

namespace EssamProject.Controllers.Filters
{
    public class CustomFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Execution started.");
            await next();
            Console.WriteLine("Execution completed.");
        }
    }
}
