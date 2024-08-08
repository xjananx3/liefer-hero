namespace LieferHero.ViewModels;

public class EditSpeiseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Beschreibung { get; set; }
    public IFormFile Bild { get; set; }
    public string? Url { get; set; }
    public decimal Preis { get; set; }
    public DateTime ErstelltAm { get; set; }
}