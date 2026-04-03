using System;
using System.Threading.Tasks;
using Action.Domain.Entities;
using Action.Infrastructure.Persistance.Context;
using CrimeAndWin.Action.GameMechanics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.API.Controllers;

[ApiController]
[Route("api/action/energy")]
public class EnergyBoostController : ControllerBase
{
    private readonly ActionDbContext _context;

    public EnergyBoostController(ActionDbContext context)
    {
        _context = context;
    }

    [HttpPost("boost")]
    public async Task<IActionResult> Boost([FromBody] BoostRequest request)
    {
        if (!EnergyConstants.ItemRefillBonus.ContainsKey(request.ItemType))
        {
            return BadRequest("Invalid item type for boost.");
        }

        var state = await _context.PlayerEnergyStates.FirstOrDefaultAsync(s => s.Id == request.PlayerId);
        if (state == null)
        {
            state = new PlayerEnergyState
            {
                Id = request.PlayerId,
                CurrentEnergy = EnergyConstants.BaseMaxEnergy,
                LastRefillAt = DateTime.UtcNow,
                CreatedAtUtc = DateTime.UtcNow,
                UpdatedAtUtc = DateTime.UtcNow
            };
            await _context.PlayerEnergyStates.AddAsync(state);
        }

        int bonusSeconds = EnergyConstants.ItemRefillBonus[request.ItemType];
        
        state.ActiveBoostItem = request.ItemType;
        state.BoostExpiresAt = DateTime.UtcNow.AddSeconds(bonusSeconds * 10); // Example: boost lasts 10x the bonus interval
        state.UpdatedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Boost activated", expiresAt = state.BoostExpiresAt });
    }
}

public record BoostRequest(Guid PlayerId, string ItemType);


