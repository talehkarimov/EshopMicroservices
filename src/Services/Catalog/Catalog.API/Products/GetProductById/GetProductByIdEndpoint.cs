
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest();
    public record GetProductByIdResponse(Product Product);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                result.Adapt<GetProductByIdResponse>();
                return Results.Ok(result);
            })
            .WithName("GetProductById")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get product by id")
            .WithDescription("Get product by id");
        }
    }
}
