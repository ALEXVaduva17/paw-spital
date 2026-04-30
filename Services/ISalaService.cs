using PawSpital.Models;

namespace PawSpital.Services;

public interface ISalaService
{
    Task<List<Sala>> GetAllAsync();
    Task<Sala?> GetByIdAsync(int id);
    Task CreateAsync(Sala model);
    Task<bool> UpdateAsync(Sala model);
    Task<bool> DeleteAsync(int id);
}

