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
        return await _context.Teacher.ToListAsync();
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        return await _context.Teacher.FindAsync(id);
    }

    public async Task AddAsync(Teacher teacher)
    {
        await _context.Teacher.AddAsync(teacher);
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        await _context.Teacher.Update(teacher);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _context.Teacher.FindAsync(id);
        if (exists == null) return false;

        _context.Teacher.Remove(exists);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}