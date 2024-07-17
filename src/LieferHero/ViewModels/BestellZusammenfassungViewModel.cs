using LieferHero.Models;

namespace LieferHero.ViewModels;

public class BestellZusammenfassungViewModel
{
    public int BestellungId { get; set; }
    public List<SpeiseInBestellung> Speisen { get; set; }
    public DateTime BestellZeit { get; set; }
    public int Gesamtkosten { get; set; }
}