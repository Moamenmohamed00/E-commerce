using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity, ISoftDeletable
    {
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = [];
        public ICollection<Product> Products { get; set; } = [];
    }
}
