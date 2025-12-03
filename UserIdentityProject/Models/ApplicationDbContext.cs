using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserIdentityProject.Models
{
    public class ApplicationDbContext:IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.MenuId });  // to composte key
            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId); //relationship
            modelBuilder.Entity<OrderItem>().HasOne(oi => oi.MenuItem).WithMany(m => m.Orders).HasForeignKey(oi => oi.MenuId);
        }
    }
}
