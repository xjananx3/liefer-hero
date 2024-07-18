using LieferHero.Data;
using LieferHero.Interfaces;
using LieferHero.Models;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Repository;

public class SpeiseRepository : ISpeiseRepository
{
    private readonly ApplicationDbContext _context;

    public SpeiseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Speise>> GetAll()
    {
        return await _context.Speisen.ToListAsync();
    }

    public async Task<Speise> GetByIdAsync(int id)
    {
        return await _context.Speisen.FirstOrDefaultAsync(s => s.Id == id) ?? throw new InvalidOperationException();
    }

    public bool Add(Speise speise)
    {
        _context.Add(speise);
        return Save();
    }

    public bool Update(Speise speise)
    {
        _context.Update(speise);
        return Save();
    }

    public bool Delete(Speise speise)
    {
        _context.Remove(speise);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}