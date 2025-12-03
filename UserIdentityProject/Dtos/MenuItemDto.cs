using System.ComponentModel.DataAnnotations;

namespace UserIdentityProject.Dtos
{
    public class MenuItemDto
    {
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        public decimal price { get; set; }

        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
