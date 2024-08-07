using System.ComponentModel.DataAnnotations;

namespace LieferHero.Models;

public class Speise
{
    [Key] 
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Beschreibung { get; set; }
    public string? Bild { get; set; }
    public decimal Preis { get; set; }
    public DateTime ErstelltAm { get; set; }
}