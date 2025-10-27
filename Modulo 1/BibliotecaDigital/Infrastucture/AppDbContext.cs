using Microsoft.EntityFrameworkCore;
using BibliotecaDigital.Models;

namespace BibliotecaDigital.Infrastucture;

public class AppDbContext : DbContext
{
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Libros> Libros { get; set; }
    public DbSet<Prestamos> Prestamos { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prestamos>()
            .HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.IdUsuario);

        modelBuilder.Entity<Prestamos>()
            .HasOne(p => p.Libro)
            .WithMany()
            .HasForeignKey(p => p.IdLibro);
    }

}