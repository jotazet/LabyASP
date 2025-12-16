using Lab0.Models;
using Lab0.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _cars;

    public CarsController(ICarService cars)
    {
        _cars = cars;
    }

    // GET: /api/cars
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetAll()
    {
        var list = await _cars.GetAllAsync();
        return Ok(list);
    }

    // GET: /api/cars/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Car>> GetById(int id)
    {
        var car = await _cars.GetByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(car);
    }

    // GET: /api/cars/by-registration/{registrationNumber}
    [HttpGet("by-registration/{registrationNumber}")]
    public async Task<ActionResult<Car>> GetByRegistration(string registrationNumber)
    {
        var car = await _cars.GetByRegistrationAsync(registrationNumber);
        if (car == null) return NotFound();
        return Ok(car);
    }

    // POST: /api/cars
    [HttpPost]
    public async Task<ActionResult<Car>> Create([FromBody] Car car)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        if (await _cars.RegistrationExistsAsync(car.RegistrationNumber))
        {
            ModelState.AddModelError(nameof(Car.RegistrationNumber), "Registration already exists");
            return ValidationProblem(ModelState);
        }
        await _cars.AddAsync(car);
        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }

    // PUT: /api/cars/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Car updated)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var existing = await _cars.GetByIdAsync(id);
        if (existing == null) return NotFound();
        if (await _cars.RegistrationExistsAsync(updated.RegistrationNumber, exceptId: id))
        {
            ModelState.AddModelError(nameof(Car.RegistrationNumber), "Registration already exists");
            return ValidationProblem(ModelState);
        }
        updated.Id = id;
        await _cars.UpdateAsync(updated);
        return NoContent();
    }

    // DELETE: /api/cars/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _cars.GetByIdAsync(id);
        if (existing == null) return NotFound();
        await _cars.DeleteAsync(id);
        return NoContent();
    }
}

