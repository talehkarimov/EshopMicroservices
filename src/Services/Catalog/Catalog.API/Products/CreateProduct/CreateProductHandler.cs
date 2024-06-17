using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(Guid Id, string Name, List<string> Category, 
        string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            Product product = MapCommandToProduct(command);

            session.Store(product);
            await session.SaveChangesAsync();

            return new CreateProductResult(product.Id);
        }

        private Product MapCommandToProduct(CreateProductCommand command)
        {
            return new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
        }
    }
}
