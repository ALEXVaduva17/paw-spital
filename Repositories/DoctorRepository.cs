using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Models;

namespace PawSpital.Repositories;

public sealed class DoctorRepository : IDoctorRepository
{
    private readonly SpitalContext _context;

    public DoctorRepository(SpitalContext context)
    {
        _context = context;
    }

    public Task<List<Doctor>> GetAllWithDepartamentAsync() =>
        _context.Doctori
            .AsNoTracking()
            .Include(d => d.Departament)
            .ToListAsync();

    public Task<Doctor?> GetByIdWithDepartamentAsync(int id) =>
        _context.Doctori
            .AsNoTracking()
            .Include(d => d.Departament)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Doctor entity)
    {
        _context.Doctori.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Doctor entity)
    {
        _context.Doctori.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Doctor entity)
    {
        _context.Doctori.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        _context.Doctori.AnyAsync(d => d.Id == id);
}

