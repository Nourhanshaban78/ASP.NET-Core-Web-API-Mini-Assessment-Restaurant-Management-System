using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentityProject.Models
{
    public class OrderItem
    {

        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public int MenuId { get; set; }
        public MenuItem MenuItem { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }


    }
}
