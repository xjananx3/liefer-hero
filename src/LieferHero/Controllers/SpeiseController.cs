using System.Security.AccessControl;
using LieferHero.Data;
using LieferHero.Interfaces;
using LieferHero.Models;
using LieferHero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Controllers;

public class SpeiseController : Controller
{
    private readonly ISpeiseRepository _speiseRepository;
    private readonly ApplicationDbContext _context;

    public SpeiseController(ISpeiseRepository speiseRepository, ApplicationDbContext context)
    {
        _speiseRepository = speiseRepository;
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var speisen = await _speiseRepository.GetAll();

        var speisenVm = speisen
            .Select(s => new SpeiseViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Preis = s.Price,
                Menge = 0
            })
            .ToList();
        
        return View(speisenVm);
    }

    public async Task<IActionResult> SpeiseZurBestellungHinzufuegen(int speiseId, int menge)
    {
        var speise = await _speiseRepository.GetByIdAsync(speiseId);
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