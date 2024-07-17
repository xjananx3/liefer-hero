using LieferHero.Data;
using LieferHero.Models;
using LieferHero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Controllers;

public class BestellungController : Controller
{
    private readonly ApplicationDbContext _context;

    public BestellungController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var speisenInBestellung = await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .ToListAsync();

        var bestellung = new Bestellung()
        {
            Speisen = speisenInBestellung,
        };
        
        return View(bestellung);
    }

    [HttpPost]
    public async Task<IActionResult> BestellungAufgeben()
    {
        var bestellSpeisen = await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .ToListAsync();
        
        if (bestellSpeisen != null)
        {
            var bestellung = new Bestellung()
            {
                Speisen = bestellSpeisen,
                BestellZeit = DateTime.Now
            };
            _context.Bestellungen.Add(bestellung);
        }
        
        await _context.SaveChangesAsync();
        
        return RedirectToAction("BestellZusammenfassung");
    }

    public async Task<IActionResult> BestellZusammenfassung(int id)
    {
        var userBestellunG = await _context.Bestellungen.FirstOrDefaultAsync(b => b.Id == id);
        var bestellSpeisen = await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .Where(be => be.BestellungId == id) // Filter nach BestellungId
            .ToListAsync();

        var bestellZusammenfassungViewModel = new BestellZusammenfassungViewModel()
        {
            BestellungId = userBestellunG.Id,
            Speisen = bestellSpeisen,
            BestellZeit = userBestellunG.BestellZeit,

        };
        
        return View(bestellZusammenfassungViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> BestellBestaetigung(int id)
    {
        var bestellSpeisen = await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .Where(be => be.BestellungId == id) // Filter nach BestellungId
            .ToListAsync();
        
        var bestellung = await _context.Bestellungen.FirstOrDefaultAsync(b => b.Id == id);

        var gesamtKosten = CalculateTotalCost(bestellSpeisen);
        var nachricht = $"Bestellung erfolgreich aufgegeben: {gesamtKosten:C}";
        
        
        var aufgegebeneBestellung = new AufgegebeneBestellung()
        {
            GesamtKosten = gesamtKosten, 
            Nachricht = nachricht,
            BestellungId = bestellung.Id,
            Bestellung = bestellung
        };
        
        _context.AufgegebeneBestellungen.Add(aufgegebeneBestellung);
        _context.SpeisenInBestellung.RemoveRange(bestellSpeisen);
        await _context.SaveChangesAsync();
        
        return View(aufgegebeneBestellung);
    }
    
    private decimal CalculateTotalCost(IEnumerable<SpeiseInBestellung> orderItems)
    {
        decimal sumCost = orderItems.Sum(o => o.Menge * o.Speise.Price);
        sumCost += sumCost * 0.1m; // Apply 10% tax
        if (sumCost > 50)
        {
            sumCost -= sumCost * 0.05m; // Apply 5% discount for orders over $50
        }
        return sumCost;
    }
}