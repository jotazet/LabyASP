using Lab0.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab0.Services;

public class EfCarService : ICarService
{
    private readonly AddDbContext _context;

    public EfCarService(AddDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await _context.Cars
            .AsNoTracking()
            .Include(c => c.Company)
            .ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await _context.Cars
            .AsNoTracking()
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Car?> GetByRegistrationAsync(string registrationNumber)
    {
        return await _context.Cars
            .AsNoTracking()
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.RegistrationNumber == registrationNumber);
    }

    public async Task AddAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is not null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteByRegistrationAsync(string registrationNumber)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.RegistrationNumber == registrationNumber);
        if (car is not null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}
