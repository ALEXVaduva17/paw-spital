using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Models;

namespace PawSpital.Repositories;

public sealed class DepartamentRepository : IDepartamentRepository
{
    private readonly SpitalContext _context;

    public DepartamentRepository(SpitalContext context)
    {
        _context = context;
    }

    public Task<List<Departament>> GetAllAsync() =>
        _context.Departamente.AsNoTracking().ToListAsync();

    public Task<Departament?> GetByIdAsync(int id) =>
        _context.Departamente.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Departament entity)
    {
        _context.Departamente.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Departament entity)
    {
        _context.Departamente.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Departament entity)
    {
        _context.Departamente.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Departamente.AnyAsync(d => d.Id == id);
}

