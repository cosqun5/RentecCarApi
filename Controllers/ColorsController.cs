using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenteCarApi.DAL.EfCore;
using RenteCarApi.Entities;
using RenteCarApi.Entities.Dtos.Colors;
using System.Net;

namespace RenteCarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        public readonly AppDbContext _context;

        public ColorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetColor")]
        public async Task<IActionResult> GetColor()
        {
            ICollection<Color> colors = await _context.Colors.ToListAsync();
            if (colors.Count == 0)
            {
                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.OK, colors);
        }
        [HttpGet("GetColorId/{id}")]

        public async Task<IActionResult> GetColorId([FromRoute] int id)
        {
            ICollection<Color> colors = await _context.Colors.Where(c => c.Id == id).ToListAsync();
            if (colors.Count == 0)
            {
                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.OK, colors);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateColorDto colordto)
        {
            Color color = new Color
            {
                Name = colordto.Name
            };
            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UpdateColorDto update)
        {
            var result = await _context.Colors.FindAsync(update.Id);
            if (result is null) return NotFound();
            result.Name = update.Name;
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Colors.FindAsync(id);
            if (result is null) return NotFound();
            _context.Colors.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
