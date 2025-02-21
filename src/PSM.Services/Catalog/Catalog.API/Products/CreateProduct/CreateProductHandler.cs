using BuildingBlocks.CQRS;
using Catalog.API.Models;


namespace Catalog.API.Products.CreateProducts;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //create Product entity from command obj
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,   
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}