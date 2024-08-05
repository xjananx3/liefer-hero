using LieferHero.Models;

namespace LieferHero.Interfaces;

public interface IAufgegebeneBestellungRepository
{
    Task<IEnumerable<AufgegebeneBestellung>> GetAll();
    Task<AufgegebeneBestellung> GetById(int id);
    bool Add(AufgegebeneBestellung aufgegebeneBestellung);
    bool Update(AufgegebeneBestellung aufgegebeneBestellung);
    bool Delete(AufgegebeneBestellung aufgegebeneBestellung);
    bool Save();
}