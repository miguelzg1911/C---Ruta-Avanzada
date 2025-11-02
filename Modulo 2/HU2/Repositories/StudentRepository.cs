using DefaultNamespace;
using HU2.Interfaces;

namespace HU2.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Student.ToListAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Student.FindAsync(id);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Student.AddAsync(student);
    }

    public async Task UpdateAsync(Student student)
    {
        await _context.Student.Update(student);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _context.Student.FindAsync(id);
        if (exists == null) return false;

        _context.Student.Remove(exists);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}