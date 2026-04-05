using Action.Domain.Entities;
using Action.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Action.API.Controllers;

[Route("api/action/admin")]
[ApiController]
public class AdminActionController(ActionDbContext context) : ControllerBase
{
    [HttpGet("energy")]
    public async Task<IActionResult> GetAllEnergyStates()
    {
        var states = await context.PlayerEnergyStates.ToListAsync();
        return Ok(states);
    }

    [HttpGet("cooldowns")]
    public async Task<IActionResult> GetActiveCooldowns()
    {
        var now = DateTime.UtcNow;
        var cooldowns = await context.PlayerActionAttempts
            .Where(x => x.CooldownEndsAt > now)
            .OrderByDescending(x => x.CooldownEndsAt)
            .ToListAsync();
        return Ok(cooldowns);
    }

    [HttpGet("logs")]
    public async Task<IActionResult> GetActionLogs(int page = 1, int pageSize = 50)
    {
        var logs = await context.PlayerActionAttempts
            .OrderByDescending(x => x.AttemptedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return Ok(logs);
    }

    [HttpGet("boosts")]
    public async Task<IActionResult> GetActiveBoosts()
    {
        var now = DateTime.UtcNow;
        var boosts = await context.PlayerEnergyStates
            .Where(x => x.BoostExpiresAt != null && x.BoostExpiresAt > now)
            .ToListAsync();
        return Ok(boosts);
    }

    [HttpDelete("boosts/{id:guid}")]
    public async Task<IActionResult> RevokeBoost(Guid id)
    {
        var state = await context.PlayerEnergyStates.FindAsync(id);
        if (state is null) return NotFound();

        state.ActiveBoostItem = null;
        state.BoostExpiresAt = null;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("energy/refill")]
    public async Task<IActionResult> ManualEnergyRefill([FromBody] RefillRequest request)
    {
        var state = await context.PlayerEnergyStates.FindAsync(request.PlayerId);
        if (state is null) return NotFound();

        state.CurrentEnergy += request.Amount;
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("BulkResetEnergy")]
    public async Task<IActionResult> BulkResetEnergy()
    {
        var states = await context.PlayerEnergyStates.ToListAsync();
        foreach (var state in states)
        {
            state.CurrentEnergy = 100; // Assuming 100 is max, or we could have a MaxEnergy field
        }
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("BulkClearCooldowns")]
    public async Task<IActionResult> BulkClearCooldowns()
    {
        var now = DateTime.UtcNow;
        var cooldowns = await context.PlayerActionAttempts
            .Where(x => x.CooldownEndsAt > now)
            .ToListAsync();
            
        foreach (var cd in cooldowns)
        {
            cd.CooldownEndsAt = now.AddSeconds(-1);
        }
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("GetServiceHealth")]
    public IActionResult GetServiceHealth()
    {
        // Simple mock of service health for the dashboard
        var services = new[]
        {
            new { ServiceName = "Identity", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "PlayerProfile", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "GameWorld", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Action", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Economy", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Inventory", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Leadership", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Notification", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Moderation", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Saga", IsHealthy = true, CheckedAt = DateTime.UtcNow },
            new { ServiceName = "Gateway", IsHealthy = true, CheckedAt = DateTime.UtcNow }
        };
        return Ok(services);
    }

    public record RefillRequest(Guid PlayerId, int Amount);
}


