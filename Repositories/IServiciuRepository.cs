using PawSpital.Models;

namespace PawSpital.Repositories;

public interface IServiciuRepository
{
    Task<List<Serviciu>> GetAllAsync();
    Task<Serviciu?> GetByIdAsync(int id);
    Task AddAsync(Serviciu entity);
    Task UpdateAsync(Serviciu entity);
    Task DeleteAsync(Serviciu entity);
    Task<bool> ExistsAsync(int id);
}

