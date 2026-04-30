using PawSpital.Models;

namespace PawSpital.Services;

public interface IServiciuService
{
    Task<List<Serviciu>> GetAllAsync();
    Task<Serviciu?> GetByIdAsync(int id);
    Task CreateAsync(Serviciu model);
    Task<bool> UpdateAsync(Serviciu model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

