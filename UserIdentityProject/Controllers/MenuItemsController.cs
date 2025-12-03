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
    public class MenuItemsController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menu = await _context.MenuItems.OrderBy(m=>m.Name).ToListAsync();
            return Ok(menu);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneItem(int id)
        {
            var menu = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            return Ok(menu);

        }

        [HttpPost]
        public async Task<IActionResult> AddNewItem(MenuItemDto dto)
        { 
            var menu = new MenuItem
            {
                Name = dto.Name,
                CategoryId =dto.CategoryId,
                Description = dto.Description,
                price = dto.price,
                CreatedAt = DateTime.Now
            };
            await _context.AddAsync(menu);
            _context.SaveChanges();
            return Ok(menu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(MenuItemDto dto , int id)
        {
            var menu = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
                return BadRequest("This Items Not Found In The Menu ");
            menu.Name = dto.Name;
            menu.CategoryId = dto.CategoryId;
            menu.Description = dto.Description;
            menu.price = dto.price;
            menu.CreatedAt = DateTime.Now;
            _context.Update(menu);
            _context.SaveChanges();
            return Ok(menu);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var menu = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
                return BadRequest("Not Found!!");
            _context.Remove(menu);
            _context.SaveChanges();
            return Ok(menu);
        }
    }
}
