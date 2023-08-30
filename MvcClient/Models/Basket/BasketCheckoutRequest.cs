using System.ComponentModel.DataAnnotations;

namespace MvcClient.Models.Basket
{
    public class BasketCheckoutRequest
    {
        [Required]
        public string? Buyer { get; set; }
        [Required]
        public IEnumerable<Guid>? ItemsToPurchase { get; set; }
    }
}
