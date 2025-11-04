using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Domain.Entities;
using PlataformaEducativa.Domain.Interfaces;
using PlataformaEducativa.Infrastructure.Data;

namespace PlataformaEducativa.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        return await _context.Teachers.FindAsync(id);
    }

    public async Task AddAsync(Teacher teacher)
    {
        await _context.Teachers.AddAsync(teacher);
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _context.Teachers.FindAsync(id);
        if (exists == null) return false;

        _context.Teachers.Remove(exists);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}