using PawSpital.Models;
using PawSpital.Repositories;

namespace PawSpital.Services;

public sealed class ProgramareService : IProgramareService
{
    private readonly IProgramareRepository _repo;

    public ProgramareService(IProgramareRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Programare>> GetAllAsync() => _repo.GetAllWithReferencesAsync();

    public Task<Programare?> GetByIdAsync(int id) => _repo.GetByIdWithReferencesAsync(id);

    public Task CreateAsync(Programare model) => _repo.AddAsync(model);

    public async Task<bool> UpdateAsync(Programare model)
    {
        if (!await _repo.ExistsAsync(model.Id))
            return false;

        await _repo.UpdateAsync(model);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdWithReferencesAsync(id);
        if (entity is null)
            return false;

        await _repo.DeleteAsync(entity);
        return true;
    }
}

