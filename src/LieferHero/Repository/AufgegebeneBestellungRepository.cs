using LieferHero.Data;
using LieferHero.Interfaces;
using LieferHero.Models;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Repository;

public class AufgegebeneBestellungRepository : IAufgegebeneBestellungRepository
{
    private readonly ApplicationDbContext _context;

    public AufgegebeneBestellungRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<AufgegebeneBestellung>> GetAll()
    {
        return await _context.AufgegebeneBestellungen.ToListAsync();
    }

    public async Task<AufgegebeneBestellung> GetById(int id)
    {
        return await _context.AufgegebeneBestellungen.FirstOrDefaultAsync(ab => ab.Id == id);
    }

    public bool Add(AufgegebeneBestellung aufgegebeneBestellung)
    {
        _context.Add(aufgegebeneBestellung);
        return Save();
    }

    public bool Update(AufgegebeneBestellung aufgegebeneBestellung)
    {
        _context.Update(aufgegebeneBestellung);
        return Save();
    }

    public bool Delete(AufgegebeneBestellung aufgegebeneBestellung)
    {
        _context.Remove(aufgegebeneBestellung);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}