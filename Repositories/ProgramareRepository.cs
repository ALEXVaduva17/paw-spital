using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Models;

namespace PawSpital.Repositories;

public sealed class ProgramareRepository : IProgramareRepository
{
    private readonly SpitalContext _context;

    public ProgramareRepository(SpitalContext context)
    {
        _context = context;
    }

    public Task<List<Programare>> GetAllWithReferencesAsync() =>
        _context.Programari
            .AsNoTracking()
            .Include(p => p.Doctor)
            .Include(p => p.Serviciu)
            .Include(p => p.Sala)
            .ToListAsync();

    public Task<Programare?> GetByIdWithReferencesAsync(int id) =>
        _context.Programari
            .AsNoTracking()
            .Include(p => p.Doctor)
            .Include(p => p.Serviciu)
            .Include(p => p.Sala)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Programare entity)
    {
        _context.Programari.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Programare entity)
    {
        _context.Programari.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Programare entity)
    {
        _context.Programari.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Programari.AnyAsync(p => p.Id == id);
}

