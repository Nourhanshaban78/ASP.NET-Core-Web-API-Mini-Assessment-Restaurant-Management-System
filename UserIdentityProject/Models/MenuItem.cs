using System.ComponentModel.DataAnnotations;

namespace UserIdentityProject.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        public decimal price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<OrderItem> Orders { get; set; } = new List<OrderItem>();
    }
}
