using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; set; }

        public User User { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public Payment? Payment { get; set; }
    }
}
