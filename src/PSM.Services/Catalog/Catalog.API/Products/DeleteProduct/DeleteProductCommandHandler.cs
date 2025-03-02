
using Catalog.API.Exception;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called with {@command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            session.Delete<Product>(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
