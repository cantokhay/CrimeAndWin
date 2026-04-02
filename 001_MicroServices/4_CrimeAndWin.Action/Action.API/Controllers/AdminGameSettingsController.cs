using System;
using System.Linq;
using System.Threading.Tasks;
using Action.Domain.Entities;
using Action.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.API.Controllers;

[ApiController]
[Route("api/admin/settings")]
public class AdminGameSettingsController : ControllerBase
{
    private readonly ActionDbContext _context;

    public AdminGameSettingsController(ActionDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.GameSettings.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Upsert([FromBody] GameSettings setting)
    {
        var existing = await _context.GameSettings.FirstOrDefaultAsync(s => s.Key == setting.Key);
        if (existing != null)
        {
            existing.Value = setting.Value;
            existing.Description = setting.Description;
            _context.GameSettings.Update(existing);
        }
        else
        {
            setting.Id = Guid.NewGuid();
            await _context.GameSettings.AddAsync(setting);
        }

        await _context.SaveChangesAsync();
        return Ok(setting);
    }
}

