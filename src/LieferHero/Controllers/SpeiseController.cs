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
    private readonly ISpeiseInBestellungRepository _speiseInBestellungRepository;


    public SpeiseController(ISpeiseRepository speiseRepository, ISpeiseInBestellungRepository speiseInBestellungRepository)
    {
        _speiseRepository = speiseRepository;
        _speiseInBestellungRepository = speiseInBestellungRepository;
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
            _speiseInBestellungRepository.Add(bestellSpeise);
        }
        return RedirectToAction("Index");
    }
}