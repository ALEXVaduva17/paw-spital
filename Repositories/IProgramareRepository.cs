using PawSpital.Models;

namespace PawSpital.Repositories;

public interface IProgramareRepository
{
    Task<List<Programare>> GetAllWithReferencesAsync();
    Task<Programare?> GetByIdWithReferencesAsync(int id);
    Task AddAsync(Programare entity);
    Task UpdateAsync(Programare entity);
    Task DeleteAsync(Programare entity);
    Task<bool> ExistsAsync(int id);
}

