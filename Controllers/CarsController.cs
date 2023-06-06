using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenteCarApi.DAL.EfCore;
using RenteCarApi.Entities;
using RenteCarApi.Entities.Dtos.Car;
using RenteCarApi.Entities.Dtos.Cars;
using System.Net;

namespace RenteCarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        public readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCar")]
        public async Task<IActionResult> GetCar()
        {
            ICollection<Car> cars = await _context.Cars.ToListAsync();
            if (cars.Count == 0)   return NotFound();
            return StatusCode((int)HttpStatusCode.OK, cars);
        }
        [HttpGet("GetCarId/{id}")]

        public async Task<IActionResult> GetCarId([FromRoute] int id)
        {
            ICollection<Car> Cars = await _context.Cars.Where(c => c.Id == id).ToListAsync();
            if (Cars.Count == 0)
            {
                return NotFound();
            }

            return StatusCode((int)HttpStatusCode.OK, Cars);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateDto Cardto)
        {
            Car Car = new Car
            {
                Name = Cardto.Name,
                ModelYear = Cardto.ModelYear,
                DailyPrice = Cardto.DailyPrice,
                Description = Cardto.Description,
                BrandId = Cardto.BrandId,
                ColorId = Cardto.ColorId,
            };
            await _context.Cars.AddAsync(Car);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarDto update)
        {
            var result = await _context.Cars.FindAsync(update.Id);
            if (result is null) return NotFound();
            result.Name = update.Name;
            result.ModelYear = update.ModelYear;
            result.DailyPrice = update.DailyPrice;
            result.Description = update.Description;
            result.BrandId = update.BrandId;
            result.ColorId = update.ColorId;
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Cars.FindAsync(id);
            if (result is null) return NotFound();
            _context.Cars.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
