using UserIdentityProject.Models;

namespace UserIdentityProject.Dtos
{
    public class OrderDto
    {
        public string UserId { get; set; }
        public bool status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
