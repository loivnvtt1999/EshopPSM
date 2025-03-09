using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: communicate with Discout.gRPC sevice
            await DeductDiscount(discountClient, command.Cart, cancellationToken);

            //Store basket in database (use Marten upsert - if exist = update, otherwise = insert)
            await repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }

        private static async Task DeductDiscount(DiscountProtoService.DiscountProtoServiceClient discountClient, ShoppingCart cart, CancellationToken cancellationToken)
        {
            // Communicate with Discount.Grpc and calculate lastest prices of products into sc
            foreach (var item in cart.Items)
            {
                var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
}
