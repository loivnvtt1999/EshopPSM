using Catalog.API.Exception;
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryResult> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with query: {query}", query);
            var products = await session.Query<Product>().Where(p=>p.Category.Contains(query.category)).ToListAsync();
            if (products == null)
            {
                //TODO: throw exception
                throw new ProductNotFoundException();
            }
            return new GetProductByCategoryResult(products);
        }
    }
}