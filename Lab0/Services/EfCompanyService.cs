using Lab0.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab0.Services;

public class EfCompanyService : ICompanyService
{
    private readonly AddDbContext _context;

    public EfCompanyService(AddDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _context.Companies.Include(c => c.Cars).ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies.Include(c => c.Cars).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company is not null)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }
    }
}

