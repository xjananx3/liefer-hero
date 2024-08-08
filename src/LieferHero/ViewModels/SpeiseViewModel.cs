namespace LieferHero.ViewModels;

public class SpeiseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Preis { get; set; }
    public string? Bild { get; set; }
    public int Menge { get; set; }
}