using LieferHero.Data;
using LieferHero.Interfaces;
using LieferHero.Models;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Repository;

public class BestellungRepository : IBestellungRepository
{
    private readonly ApplicationDbContext _context;

    public BestellungRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Bestellung>> GetAll()
    {
        return await _context.Bestellungen.ToListAsync();
    }

    public async Task<Bestellung> GetById(int id)
    {
        return await _context.Bestellungen.FirstOrDefaultAsync(b => b.Id == id);
    }

    public bool Add(Bestellung bestellung)
    {
        _context.Add(bestellung);
        return Save();
    }

    public bool Update(Bestellung bestellung)
    {
        _context.Update(bestellung);
        return Save();
    }

    public bool Delete(Bestellung bestellung)
    {
        _context.Remove(bestellung);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}