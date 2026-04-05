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

    [HttpGet("GetGlobalSettings")]
    public async Task<IActionResult> GetGlobalSettings()
    {
        var settings = await _context.GameSettings.ToListAsync();
        
        var result = new 
        {
            IsMaintenanceMode = settings.FirstOrDefault(x => x.Key == "IsMaintenanceMode")?.Value == "true",
            SuccessRateMultiplier = double.TryParse(settings.FirstOrDefault(x => x.Key == "SuccessRateMultiplier")?.Value, out var srm) ? srm : 1.0,
            CooldownMultiplier = double.TryParse(settings.FirstOrDefault(x => x.Key == "CooldownMultiplier")?.Value, out var cm) ? cm : 1.0,
            MinEnergyRequired = int.TryParse(settings.FirstOrDefault(x => x.Key == "MinEnergyRequired")?.Value, out var mer) ? mer : 5,
            GlobalAnnouncement = settings.FirstOrDefault(x => x.Key == "GlobalAnnouncement")?.Value ?? ""
        };

        return Ok(result);
    }

    [HttpPost("UpdateGlobalSettings")]
    public async Task<IActionResult> UpdateGlobalSettings([FromBody] GlobalSettingsUpdateRequest request)
    {
        await UpdateSetting("IsMaintenanceMode", request.IsMaintenanceMode.ToString().ToLower());
        await UpdateSetting("SuccessRateMultiplier", request.SuccessRateMultiplier.ToString());
        await UpdateSetting("CooldownMultiplier", request.CooldownMultiplier.ToString());
        await UpdateSetting("MinEnergyRequired", request.MinEnergyRequired.ToString());
        await UpdateSetting("GlobalAnnouncement", request.GlobalAnnouncement);

        await _context.SaveChangesAsync();
        return Ok(new { success = true });
    }

    private async Task UpdateSetting(string key, string value)
    {
        var setting = await _context.GameSettings.FirstOrDefaultAsync(x => x.Key == key);
        if (setting == null)
        {
            _context.GameSettings.Add(new GameSettings { Key = key, Value = value });
        }
        else
        {
            setting.Value = value;
        }
    }

    public class GlobalSettingsUpdateRequest
    {
        public bool IsMaintenanceMode { get; set; }
        public double SuccessRateMultiplier { get; set; }
        public double CooldownMultiplier { get; set; }
        public int MinEnergyRequired { get; set; }
        public string GlobalAnnouncement { get; set; } = "";
    }
}


