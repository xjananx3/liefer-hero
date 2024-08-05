using LieferHero.Data;
using LieferHero.Interfaces;
using LieferHero.Models;
using LieferHero.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LieferHero.Controllers;

public class BestellungController : Controller
{
    private readonly IBestellungRepository _bestellungRepository;
    private readonly IAufgegebeneBestellungRepository _aufgegebeneBestellungRepository;
    private readonly ISpeiseInBestellungRepository _speiseInBestellungRepository;

    public BestellungController(IBestellungRepository bestellungRepository, IAufgegebeneBestellungRepository aufgegebeneBestellungRepository, ISpeiseInBestellungRepository speiseInBestellungRepository)
    {
        _bestellungRepository = bestellungRepository;
        _aufgegebeneBestellungRepository = aufgegebeneBestellungRepository;
        _speiseInBestellungRepository = speiseInBestellungRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var speisenInBestellung = await _speiseInBestellungRepository.GetAll();

        var bestellung = new Bestellung()
        {
            Speisen = speisenInBestellung.ToList(),
        };
        
        return View(bestellung);
    }

    [HttpPost]
    public async Task<IActionResult> BestellungAufgeben()
    {
        var bestellSpeisen = await _speiseInBestellungRepository.GetAll();
        
        if (bestellSpeisen != null)
        {
            var bestellung = new Bestellung()
            {
                Speisen = bestellSpeisen.ToList(),
                BestellZeit = DateTime.Now
            };
            _bestellungRepository.Add(bestellung);
        }
        
        return RedirectToAction("BestellZusammenfassung");
    }

    public async Task<IActionResult> BestellZusammenfassung(int id)
    {
        var userBestellung = await _bestellungRepository.GetById(id);
        var bestellSpeisen = await _speiseInBestellungRepository.GetByOrderIdAsync(id);
        var bestellZusammenfassungViewModel = new BestellZusammenfassungViewModel()
        {
            BestellungId = userBestellung.Id,
            Speisen = bestellSpeisen.ToList(),
            BestellZeit = userBestellung.BestellZeit,
        };
        
        return View(bestellZusammenfassungViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> BestellBestaetigung(int id)
    {
        var bestellSpeisen = await _speiseInBestellungRepository.GetByOrderIdAsync(id);
        
        var bestellung = await _bestellungRepository.GetById(id);
        var gesamtKosten = CalculateTotalCost(bestellSpeisen);
        var nachricht = $"Bestellung erfolgreich aufgegeben: {gesamtKosten:C}";
        
        var aufgegebeneBestellung = new AufgegebeneBestellung()
        {
            GesamtKosten = gesamtKosten, 
            Nachricht = nachricht,
            BestellungId = bestellung.Id,
            Bestellung = bestellung
        };
        _aufgegebeneBestellungRepository.Add(aufgegebeneBestellung);
        _speiseInBestellungRepository.DeleteAll(bestellSpeisen);
        
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