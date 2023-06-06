using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenteCarApi.DAL.EfCore;
using RenteCarApi.Entities;
using RenteCarApi.Entities.Dtos.Brands;
using System.Net;

namespace RenteCarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        public readonly AppDbContext _context;

        public BrandsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand()
        {
            ICollection<Brand> Brands = await _context.Brands.ToListAsync();
            if (Brands.Count == 0)
            {
                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.OK, Brands);
        }
        [HttpGet("GetBrandId/{id}")]

        public async Task<IActionResult> GetBrandId([FromRoute] int id)
        {
            ICollection<Brand> Brands = await _context.Brands.Where(c => c.Id == id).ToListAsync();
            if (Brands.Count == 0)
            {
                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.OK, Brands);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandDto Branddto)
        {
            Brand Brand = new Brand
            {
                Name = Branddto.Name
            };
            await _context.Brands.AddAsync(Brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UpdateBrandDto update)
        {
            var result = await _context.Brands.FindAsync(update.Id);
            if (result is null) return NotFound();
            result.Name = update.Name;
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Brands.FindAsync(id);
            if (result is null) return NotFound();
            _context.Brands.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
