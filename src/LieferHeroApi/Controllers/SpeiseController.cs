using LieferHero.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LieferHeroApi.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class SpeiseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SpeiseController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Speisekarte()
    {
        var speisekarte = _context.Speisen.ToList();
        
        return Ok(speisekarte);
    }
}