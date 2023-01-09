using MvcClient.Models.Basket;

namespace MvcClient.Dtos.Basket
{
    public class BasketCheckoutDto
    {
        public string Buyer { get; set; } = string.Empty;
        public IEnumerable<Guid> ItemsToPurchase { get; set; }

        public BasketCheckoutDto() { }
        public BasketCheckoutDto(BasketCheckoutRequest basketCheckoutRequest)
        {
            Buyer = basketCheckoutRequest.Buyer!;
            ItemsToPurchase = basketCheckoutRequest.ItemsToPurchase!;
        }
    }
}
