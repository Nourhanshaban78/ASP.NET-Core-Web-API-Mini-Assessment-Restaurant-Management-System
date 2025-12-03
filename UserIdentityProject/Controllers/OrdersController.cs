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
    public class OrdersController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var order = await _context.Orders.Include(o=>o.Items).ToListAsync();
            return Ok(order);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneOrder(int id)
        {
            var order = await _context.Orders.Include(o=>o.Items).SingleOrDefaultAsync(o=>o.Id == id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                status = dto.status,
                TotalPrice = dto.TotalPrice,
                CreatedAt = DateTime.Now
            };
            await _context.AddAsync(order);
            _context.SaveChanges();
            return Ok(order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrder(OrderDto dto,int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return BadRequest($"This Order Not Found :{id}");
            order.UserId = dto.UserId;
            order.status = dto.status;
            order.TotalPrice = dto.TotalPrice;
            order.CreatedAt = DateTime.Now;
            _context.Update(order);
            _context.SaveChanges();
            return Ok(order);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder (int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return BadRequest("This Order Is Not Found!");
            _context.Remove(order);
            _context.SaveChanges();
            return Ok(order);
        }
    }
}
