using Lab0.Models;

namespace Lab0.Services;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task DeleteAsync(int id);
}
