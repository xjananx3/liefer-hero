using LieferHero.Models;

namespace LieferHero.Interfaces;

public interface ISpeiseInBestellungRepository
{
    Task<IEnumerable<SpeiseInBestellung>> GetAll();
    Task<SpeiseInBestellung> GetByIdAsync(int id);
    Task<IEnumerable<SpeiseInBestellung>> GetByOrderIdAsync(int orderId);
    bool Add(SpeiseInBestellung speiseInBestellung);
    bool Update(SpeiseInBestellung speiseInBestellung);
    bool Delete(SpeiseInBestellung speiseInBestellung);
    bool DeleteAll(IEnumerable<SpeiseInBestellung> speisenInBestellung);
    bool Save();
}