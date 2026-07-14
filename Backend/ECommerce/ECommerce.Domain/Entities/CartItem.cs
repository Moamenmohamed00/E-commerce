using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; } = null!;
        public ProductVariant ProductVariant { get; set; } = null!;
    }
}
