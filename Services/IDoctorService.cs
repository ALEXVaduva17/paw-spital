using PawSpital.Models;

namespace PawSpital.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task CreateAsync(Doctor model);
    Task<bool> UpdateAsync(Doctor model);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

