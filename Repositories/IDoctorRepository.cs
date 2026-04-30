using PawSpital.Models;

namespace PawSpital.Repositories;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllWithDepartamentAsync();
    Task<Doctor?> GetByIdWithDepartamentAsync(int id);
    Task AddAsync(Doctor entity);
    Task UpdateAsync(Doctor entity);
    Task DeleteAsync(Doctor entity);
    Task<bool> ExistsAsync(int id);
}

