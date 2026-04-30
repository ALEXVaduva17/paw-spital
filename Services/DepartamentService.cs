using PawSpital.Models;
using PawSpital.Repositories;

namespace PawSpital.Services;

public sealed class DepartamentService : IDepartamentService
{
    private readonly IDepartamentRepository _repo;

    public DepartamentService(IDepartamentRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Departament>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Departament?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(Departament model) => _repo.AddAsync(model);

    public async Task<bool> UpdateAsync(Departament model)
    {
        if (!await _repo.ExistsAsync(model.Id))
            return false;

        await _repo.UpdateAsync(model);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            return false;

        await _repo.DeleteAsync(entity);
        return true;
    }

    public Task<bool> ExistsAsync(int id) => _repo.ExistsAsync(id);
}

