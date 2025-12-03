using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserIdentityProject.Dtos;
using UserIdentityProject.Models;

namespace UserIdentityProject.Controllers
{
    [Authorize (Roles = "Admin") ]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var category = await _context.Categories.OrderBy(c=>c.Name).ToListAsync();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneCategory(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id== id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(CategoriesDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
            };
            await _context.AddAsync(category);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(CategoriesDto dto, int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return BadRequest($"This ID {id} Is Not Found!");
            category.Name = dto.Name;
            _context.Update(category);
            _context.SaveChanges();
            return Ok(category);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return BadRequest($"This ID {id} Is Not Found!");
            _context.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }


    }
}
