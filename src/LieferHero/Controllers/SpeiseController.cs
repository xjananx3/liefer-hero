using LieferHero.Data;
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
        var speisen = await _context.Speisen.ToListAsync();
        var speisenVm = new List<SpeiseViewModel>();

        foreach (var speise in speisen)
        {
            var speiseVm = new SpeiseViewModel()
            {
                Id = speise.Id,
                Name = speise.Name,
                Preis = speise.Price,
                Menge = 0
            };
            speisenVm.Add(speiseVm);
        }
        return View(speisenVm);
    }
}