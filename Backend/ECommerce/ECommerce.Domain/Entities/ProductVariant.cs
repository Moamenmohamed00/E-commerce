using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class ProductVariant : BaseEntity, ISoftDeletable
    {
        public int ProductId { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Product Product { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
        public ICollection<WishlistItem> WishlistItems { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
