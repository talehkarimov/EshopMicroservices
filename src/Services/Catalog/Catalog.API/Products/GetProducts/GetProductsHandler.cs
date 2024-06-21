using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10,cancellationToken);
            return new GetProductResult(products);
        }
    }
}
