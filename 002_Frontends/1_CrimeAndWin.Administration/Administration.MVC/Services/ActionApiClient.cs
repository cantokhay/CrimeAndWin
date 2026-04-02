using Administration.MVC.Services.Dtos;
using System.Net.Http.Json;

namespace Administration.MVC.Services;

public class ActionApiClient(HttpClient http)
{
    public async Task<List<PlayerEnergyDto>> GetAllEnergyStatesAsync()
    {
        try { return (await http.GetFromJsonAsync<List<PlayerEnergyDto>>("/api/action/admin/energy")) ?? []; }
        catch { return []; }
    }

    public async Task<List<CooldownDto>> GetActiveCooldownsAsync()
    {
        try { return (await http.GetFromJsonAsync<List<CooldownDto>>("/api/action/admin/cooldowns")) ?? []; }
        catch { return []; }
    }

    public async Task<List<ActionLogDto>> GetActionLogsAsync(int page = 1, int pageSize = 50)
    {
        try { return (await http.GetFromJsonAsync<List<ActionLogDto>>($"/api/action/admin/logs?page={page}&pageSize={pageSize}")) ?? []; }
        catch { return []; }
    }

    public async Task<List<ActiveBoostDto>> GetActiveBoostsAsync()
    {
        try { return (await http.GetFromJsonAsync<List<ActiveBoostDto>>("/api/action/admin/boosts")) ?? []; }
        catch { return []; }
    }

    public async Task RevokeBoostAsync(Guid playerId)
    {
        try { await http.DeleteAsync($"/api/action/admin/boosts/{playerId}"); }
        catch { /* servis kapalıysa yoksay */ }
    }

    public async Task ManualEnergyRefillAsync(Guid playerId, int amount)
    {
        try { await http.PostAsJsonAsync("/api/action/admin/energy/refill", new { playerId, amount }); }
        catch { /* servis kapalıysa yoksay */ }
    }
}
