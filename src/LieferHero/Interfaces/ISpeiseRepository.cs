using LieferHero.Models;

namespace LieferHero.Interfaces;

public interface ISpeiseRepository
{
    Task<IEnumerable<Speise>> GetAll();
    Task<Speise> GetByIdAsync(int id);
    bool Add(Speise speise);
    bool Update(Speise speise);
    bool Delete(Speise speise);
    bool Save();
}