namespace ECommerce.Domain.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductVariantId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
        public ProductVariant ProductVariant { get; set; } = null!;
    }
}
