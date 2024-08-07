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
    private readonly IPhotoService _photoService;


    public SpeiseController(ISpeiseRepository speiseRepository, ISpeiseInBestellungRepository speiseInBestellungRepository, IPhotoService photoService)
    {
        _speiseRepository = speiseRepository;
        _speiseInBestellungRepository = speiseInBestellungRepository;
        _photoService = photoService;
    }
    
    public async Task<IActionResult> Index()
    {
        var speisen = await _speiseRepository.GetAll();

        var speisenVm = speisen
            .Select(s => new SpeiseViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Preis = s.Preis,
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSpeiseViewModel speiseVm)
    {
        if (ModelState.IsValid)
        {
            var result = await _photoService.AddPhotoAsync(speiseVm.Bild);

            var speise = new Speise()
            {
                Name = speiseVm.Name,
                Beschreibung = speiseVm.Beschreibung,
                Preis = speiseVm.Preis,
                Bild = result.Url.ToString(),
                ErstelltAm = speiseVm.ErstelltAm
            };
            _speiseRepository.Add(speise);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Bild hochladen, fehlgeschlagen");
        }
        return View(speiseVm);
    }
}