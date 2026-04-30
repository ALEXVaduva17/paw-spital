using PawSpital.Models;
using PawSpital.Repositories;

namespace PawSpital.Services;

public sealed class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;

    public DoctorService(IDoctorRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Doctor>> GetAllAsync() => _repo.GetAllWithDepartamentAsync();

    public Task<Doctor?> GetByIdAsync(int id) => _repo.GetByIdWithDepartamentAsync(id);

    public Task CreateAsync(Doctor model) => _repo.AddAsync(model);

    public async Task<bool> UpdateAsync(Doctor model)
    {
        if (!await _repo.ExistsAsync(model.Id))
            return false;

        await _repo.UpdateAsync(model);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdWithDepartamentAsync(id);
        if (entity is null)
            return false;

        await _repo.DeleteAsync(entity);
        return true;
    }

    public Task<bool> ExistsAsync(int id) => _repo.ExistsAsync(id);
}

