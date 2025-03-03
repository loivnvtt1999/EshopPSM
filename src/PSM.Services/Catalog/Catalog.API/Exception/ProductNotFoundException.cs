
using BuildingBlocks.Exceptions;

namespace Catalog.API.Exception
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(Guid id) : base("Product", id)
        {
        }
    }
}