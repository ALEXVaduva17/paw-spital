using PawSpital.Models;

namespace PawSpital.Repositories;

public interface IDepartamentRepository
{
    Task<List<Departament>> GetAllAsync();
    Task<Departament?> GetByIdAsync(int id);
    Task AddAsync(Departament entity);
    Task UpdateAsync(Departament entity);
    Task DeleteAsync(Departament entity);
    Task<bool> ExistsAsync(int id);
}

