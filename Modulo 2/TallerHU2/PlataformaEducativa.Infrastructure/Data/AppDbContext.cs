using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Domain.Entities;

namespace PlataformaEducativa.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }
    public DbSet<Course> Courses { get; set; }
}