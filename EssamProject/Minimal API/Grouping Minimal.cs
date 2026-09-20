namespace EssamProject.Minimal_API
{
    public static class Grouping_Minimal
    {
        public static RouteGroupBuilder MapGroupingMinimal(this IEndpointRouteBuilder group)
        {
            var productGroup = group.MapGroup("Api/Product");

            productGroup.MapGet("/Grouping", getproducts);

            return productGroup;
        }


        private static IResult getproducts()
        {
            // Implementation for getting products
            return Results.Ok(new List<object>
            {
                new  { Id = 1, Name = "Product 1", Price = 10.0m },
                new  { Id = 2, Name = "Product 2", Price = 20.0m },
                new  { Id = 3, Name = "Product 3", Price = 30.0m }
            });
        }
    }
}
