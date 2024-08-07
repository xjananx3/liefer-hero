namespace LieferHero.Models;

public class SpeiseInBestellung
{
    public int Id { get; set; }
    public Speise Speise { get; set; }
    public int Menge { get; set; }
    public int? BestellungId { get; set; }
}