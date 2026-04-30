using PawSpital.Models;

namespace PawSpital.Repositories;

public interface ISalaRepository
{
    Task<List<Sala>> GetAllAsync();
    Task<Sala?> GetByIdAsync(int id);
    Task AddAsync(Sala entity);
    Task UpdateAsync(Sala entity);
    Task DeleteAsync(Sala entity);
    Task<bool> ExistsAsync(int id);
}

