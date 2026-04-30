using PawSpital.Models;

namespace PawSpital.Services;

public interface IDepartamentService
{
    Task<List<Departament>> GetAllAsync();
    Task<Departament?> GetByIdAsync(int id);
    Task CreateAsync(Departament model);
    Task<bool> UpdateAsync(Departament model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

