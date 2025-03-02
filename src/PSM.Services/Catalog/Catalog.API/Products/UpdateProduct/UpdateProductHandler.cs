

using Catalog.API.Exception;

namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult (bool IsSuccess);
    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductResult> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called with {@command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = command.Name;
            product.Price = command.Price;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            product.Description = command.Description;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
