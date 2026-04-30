using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Models;

namespace PawSpital.Repositories;

public sealed class ServiciuRepository : IServiciuRepository
{
    private readonly SpitalContext _context;

    public ServiciuRepository(SpitalContext context)
    {
        _context = context;
    }

    public Task<List<Serviciu>> GetAllAsync() =>
        _context.Servicii.AsNoTracking().ToListAsync();

    public Task<Serviciu?> GetByIdAsync(int id) =>
        _context.Servicii.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

    public async Task AddAsync(Serviciu entity)
    {
        _context.Servicii.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Serviciu entity)
    {
        _context.Servicii.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Serviciu entity)
    {
        _context.Servicii.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Servicii.AnyAsync(s => s.Id == id);
}

