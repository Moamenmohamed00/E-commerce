using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class Address : BaseEntity, ISoftDeletable
    {
        public int UserId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = [];
    }
}
