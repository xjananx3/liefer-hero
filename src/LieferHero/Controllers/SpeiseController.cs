using System.Security.AccessControl;
using LieferHero.Data;
using LieferHero.Models;
using LieferHero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Controllers;

public class SpeiseController : Controller
{
    private readonly ApplicationDbContext _context;

    public SpeiseController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var speisenVm = await _context.Speisen
            .Select(s => new SpeiseViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Preis = s.Price,
                Menge = 0
            })
            .ToListAsync();
        return View(speisenVm);
    }

    public async Task<IActionResult> SpeiseZurBestellungHinzufuegen(int speiseId, int menge)
    {
        var speise = await _context.Speisen.FindAsync(speiseId);
        if (speise != null)
        {
            var bestellSpeise = new SpeiseInBestellung()
            {
                Speise = speise,
                Menge = menge
            };
            _context.SpeisenInBestellung.Add(bestellSpeise);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}