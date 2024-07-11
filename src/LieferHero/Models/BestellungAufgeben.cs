namespace LieferHero.Models;

public class BestellungAufgeben
{
    public int Id { get; set; }
    public decimal GesamtKosten { get; set; }
    public string Nachricht { get; set; }
}