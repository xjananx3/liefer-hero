using LieferHero.Models;

namespace LieferHero.Interfaces;

public interface IBestellungRepository
{
    Task<IEnumerable<Bestellung>> GetAll();
    Task<Bestellung> GetById(int id);
    bool Add(Bestellung bestellung);
    bool Update(Bestellung bestellung);
    bool Delete(Bestellung bestellung);
    bool Save();
}