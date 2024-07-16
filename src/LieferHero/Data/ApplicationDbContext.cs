using LieferHero.Models;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Speise> Speisen { get; set; }
    public DbSet<Bestellung> Bestellungen { get; set; }
    public DbSet<SpeiseInBestellung> SpeisenInBestellung { get; set; }
    public DbSet<AufgegebeneBestellung> AufgegebeneBestellungen { get; set; }
}