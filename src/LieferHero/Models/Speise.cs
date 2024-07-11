using System.ComponentModel.DataAnnotations;

namespace LieferHero.Models;

public class Speise
{
    [Key] 
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ErstelltAm { get; set; }
}