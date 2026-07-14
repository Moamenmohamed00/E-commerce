using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class Brand : BaseEntity, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Product> Products { get; set; } = [];
    }
}
