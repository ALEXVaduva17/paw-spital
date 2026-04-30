using PawSpital.Models;
using PawSpital.Repositories;

namespace PawSpital.Services;

public sealed class SalaService : ISalaService
{
    private readonly ISalaRepository _repo;

    public SalaService(ISalaRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Sala>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Sala?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task CreateAsync(Sala model) => _repo.AddAsync(model);

    public async Task<bool> UpdateAsync(Sala model)
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
}

