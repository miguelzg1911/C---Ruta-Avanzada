using Microsoft.EntityFrameworkCore;
using SimulacroPrueba.Models;

namespace SimulacroPrueba.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<GestionVuelos> GestionVuelos { get; set; } 
    public DbSet<Pasajeros> Pasajeros { get; set; } 
    public DbSet<ReservasVuelos> ReservasVuelos { get; set; }
}
    