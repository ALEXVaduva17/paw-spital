using PawSpital.Models;

namespace PawSpital.Services;

public interface IProgramareService
{
    Task<List<Programare>> GetAllAsync();
    Task<Programare?> GetByIdAsync(int id);
    Task CreateAsync(Programare model);
    Task<bool> UpdateAsync(Programare model);
    Task<bool> DeleteAsync(int id);
}

