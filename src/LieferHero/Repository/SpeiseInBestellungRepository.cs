using LieferHero.Data;
using LieferHero.Interfaces;
using LieferHero.Models;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Repository;

public class SpeiseInBestellungRepository : ISpeiseInBestellungRepository
{
    private readonly ApplicationDbContext _context;

    public SpeiseInBestellungRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SpeiseInBestellung>> GetAll()
    {
        return await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .ToListAsync();
    }

    public async Task<SpeiseInBestellung> GetByIdAsync(int id)
    {
        return await _context.SpeisenInBestellung.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<SpeiseInBestellung>> GetByOrderIdAsync(int orderId)
    {
        return await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .Where(be => be.BestellungId == orderId)
            .ToListAsync();
    }

    public bool Add(SpeiseInBestellung speiseInBestellung)
    {
        _context.Add(speiseInBestellung);
        return Save();
    }

    public bool Update(SpeiseInBestellung speiseInBestellung)
    {
        _context.Update(speiseInBestellung);
        return Save();
    }

    public bool Delete(SpeiseInBestellung speiseInBestellung)
    {
        _context.Update(speiseInBestellung);
        return Save();
    }

    public bool DeleteAll(IEnumerable<SpeiseInBestellung> speisenInBestellung)
    {
        _context.RemoveRange(speisenInBestellung);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}