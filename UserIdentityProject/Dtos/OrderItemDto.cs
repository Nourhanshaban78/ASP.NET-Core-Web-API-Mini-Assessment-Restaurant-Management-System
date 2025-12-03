using UserIdentityProject.Models;

namespace UserIdentityProject.Dtos
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
    }
}
