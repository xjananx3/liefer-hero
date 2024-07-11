namespace LieferHero.Models;

public class Bestellung
{
    public int Id { get; set; }
    public List<SpeiseInBestellung> Speisen { get; set; }
    public DateTime BestellZeit { get; set; }
}