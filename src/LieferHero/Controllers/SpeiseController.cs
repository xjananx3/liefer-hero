using System.Security.AccessControl;
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
}