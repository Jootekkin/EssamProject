namespace EssamProject.Controllers.VersioningURL.Filters
{
    public class MinimalApiFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Console.WriteLine("Execution started.");
            var result = await next(context);
            Console.WriteLine("Execution completed.");
            return result;
        }
    }
}
