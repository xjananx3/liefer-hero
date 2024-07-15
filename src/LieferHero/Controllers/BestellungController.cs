using LieferHero.Data;
using LieferHero.Models;
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
        var bestellSpeisen = await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .ToListAsync();

        var bestellung = new Bestellung()
        {
            Speisen = bestellSpeisen,
            BestellZeit = DateTime.Now
        };
        
        return View(bestellung);
    }

    [HttpPost]
    public async Task<IActionResult> BestellungAufgeben()
    {
        var bestellSpeisen = await _context.SpeisenInBestellung
            .Include(be => be.Speise)
            .ToListAsync();

        var gesamtKosten = CalculateTotalCost(bestellSpeisen);
        
        _context.SpeisenInBestellung.RemoveRange(bestellSpeisen);
        await _context.SaveChangesAsync();

        TempData["Message"] = $"Order placed successfully! Total Cost: {gesamtKosten:C}";
        return RedirectToAction("BestellBestaetigung");
    }

    public IActionResult BestellBestaetigung()
    {
        return View();
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