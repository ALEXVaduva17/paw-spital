using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Models;

namespace PawSpital.Repositories;

public sealed class SalaRepository : ISalaRepository
{
    private readonly SpitalContext _context;

    public SalaRepository(SpitalContext context)
    {
        _context = context;
    }

    public Task<List<Sala>> GetAllAsync() =>
        _context.Salii.AsNoTracking().ToListAsync();

    public Task<Sala?> GetByIdAsync(int id) =>
        _context.Salii.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

    public async Task AddAsync(Sala entity)
    {
        _context.Salii.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sala entity)
    {
        _context.Salii.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Sala entity)
    {
        _context.Salii.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Salii.AnyAsync(s => s.Id == id);
}

