using Lab0.Models;

namespace Lab0.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<Car?> GetByRegistrationAsync(string registrationNumber);
    Task AddAsync(Car car);
    Task UpdateAsync(Car car);
    Task DeleteAsync(int id);
    Task DeleteByRegistrationAsync(string registrationNumber);
    Task<bool> RegistrationExistsAsync(string registrationNumber, int? exceptId = null);
}
