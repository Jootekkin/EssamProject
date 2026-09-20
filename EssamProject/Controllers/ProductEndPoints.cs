namespace EssamProject.Controllers
{
    public static class ProductEndPoints
    {
        public static RouteGroupBuilder MapProductEndPoints(this IEndpointRouteBuilder group)
        {
            var productGroup = group.MapGroup("/api/products");
            productGroup.MapGet("/list", GetAllProducts);

            return productGroup;
        }

        private static IResult GetAllProducts(HttpContext context)
        {
            return Results.Ok(new List<object>
            {
                new  { Id = 1, Name = "Product 1", Price = 10.0m },
                new  { Id = 2, Name = "Product 2", Price = 20.0m },
                new  { Id = 3, Name = "Product 3", Price = 30.0m }
            });
        }
    }
}
