using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Domain.Entities;
using PlataformaEducativa.Domain.Interfaces;
using PlataformaEducativa.Infrastructure.Data;

namespace PlataformaEducativa.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public async Task UpdateAsync(Student student)
    { 
        _context.Students.Update(student);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _context.Students.FindAsync(id);
        if (exists == null) return false;

        _context.Students.Remove(exists);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}