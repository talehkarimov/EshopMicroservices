using Marten;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category,
        string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidtor : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidtor()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null) throw new ProductNotFoundException(command.Id);

            var mappedProduct = MapCommandToProduct(product,command);

            session.Update(mappedProduct);

            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }

        private Product MapCommandToProduct(Product product, UpdateProductCommand command)
        {
            product.Name = command.Name;
            product.Description = command.Description;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            return product;
        }
    }
}
