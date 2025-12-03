using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserIdentityProject.Dtos;
using UserIdentityProject.Models;

namespace UserIdentityProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var  items = await _context.OrderItems
                .Include(i=>i.MenuItem).Include(i=>i.Order).ToListAsync();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task <IActionResult> GetOneItem(int id)
        {
            var item = await _context.OrderItems.
                Include(i => i.Order).Include(i => i.MenuItem.Name).SingleOrDefaultAsync(i => i.Id == id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem(OrderItemDto dto)
        {
            var exists = await _context.OrderItems
                .AnyAsync(x => x.OrderId == dto.OrderId && x.MenuId == dto.MenuId);
            if (exists)
                return BadRequest("This menu item already exists in this order");
            var item = new OrderItem
            {
                OrderId = dto.OrderId,
                MenuId = dto.MenuId,
                Quantity = dto.Quantity,
                PriceAtPurchase = dto.PriceAtPurchase
            };
            await _context.AddAsync(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrderItem(OrderItemDto dto ,int id)
        {
            var item = await _context.OrderItems.SingleOrDefaultAsync(i => i.Id == id);
            if (item == null)
                return BadRequest("this Order Item Not Found!");
            item.MenuId = dto.MenuId;
            item.OrderId = dto.OrderId;
            item.Quantity = dto.Quantity;
            item.PriceAtPurchase = dto.PriceAtPurchase;
            _context.Update(item);
            _context.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var item = await _context.OrderItems.SingleOrDefaultAsync(i => i.Id == id);
            if (item == null)
                return BadRequest($"this Order Not Found :{id}");
            _context.Remove(item);
            _context.SaveChanges();
            return Ok(item);
        }
    }
}
