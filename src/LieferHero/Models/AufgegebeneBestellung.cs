using System.ComponentModel.DataAnnotations.Schema;

namespace LieferHero.Models;

public class AufgegebeneBestellung
{
    public int Id { get; set; }
    public decimal GesamtKosten { get; set; }
    public string Nachricht { get; set; }
    
    [ForeignKey("Bestellung")] 
    public int BestellungId { get; set; }
    public Bestellung Bestellung { get; set; }
}