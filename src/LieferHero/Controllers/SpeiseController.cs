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
        
        return View(speisen);
    }

    public async Task<IActionResult> Speisekarte()
    {
        var speisen = await _speiseRepository.GetAll();

        var speisekarte = speisen
            .Select(s => new SpeiseViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Preis = s.Preis,
                Menge = 0
            })
            .ToList();
        
        return View(speisekarte);
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

    public async Task<IActionResult> Edit(int id)
    {
        var speise = await _speiseRepository.GetByIdAsync(id);
        if (speise == null)
            return View("Error");

        var speiseVm = new EditSpeiseViewModel()
        {
            Name = speise.Name,
            Beschreibung = speise.Beschreibung ?? null,
            Url = speise.Bild,
            Preis = speise.Preis
        };
        return View(speiseVm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditSpeiseViewModel speiseVm)
    {
        if(!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Speise bearbeiten ist fehlgeschlagen...");
            return View("Edit", speiseVm);
        }

        var userSpeise = await _speiseRepository.GetByIdAsync(id);

        if (userSpeise != null)
        {
            try
            {
                await _photoService.DeletePhotoAsync(userSpeise.Bild);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bild konnte nicht gelöscht werden...");
                return View(speiseVm);
            }

            var photoResult = await _photoService.AddPhotoAsync(speiseVm.Bild);

            var speise = new Speise()
            {
                Id = id,
                Name = speiseVm.Name,
                Beschreibung = speiseVm.Beschreibung,
                Bild = photoResult.Url.ToString(),
                Preis = speiseVm.Preis,
                ErstelltAm = speiseVm.ErstelltAm
            };
            _speiseRepository.Update(speise);

            return RedirectToAction("Index");
        }

        return View(speiseVm);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var speiseDetails = await _speiseRepository.GetByIdAsync(id);
        if (speiseDetails == null)
            return View("Error");
        return View(speiseDetails);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteSpeise(int id)
    {
        var speiseDetails = await _speiseRepository.GetByIdAsync(id);

        if (speiseDetails == null)
            return View("Error");

        if (!string.IsNullOrWhiteSpace(speiseDetails.Bild)) _ = _photoService.DeletePhotoAsync(speiseDetails.Bild);

        _speiseRepository.Delete(speiseDetails);
        return View(speiseDetails);
    }
}