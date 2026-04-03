using System.Linq;
using System.Threading.Tasks;
using Action.Application.Abstract;
using Action.Domain.Entities;
using Action.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Action.Infrastructure.Services;

public class GameSettingsService : IGameSettingsService
{
    private readonly ActionDbContext _context;

    public GameSettingsService(ActionDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetIntSettingAsync(string key, int defaultValue)
    {
        var setting = await _context.GameSettings.FirstOrDefaultAsync(s => s.Key == key);
        return setting != null && int.TryParse(setting.Value, out var val) ? val : defaultValue;
    }

    public async Task<double> GetDoubleSettingAsync(string key, double defaultValue)
    {
        var setting = await _context.GameSettings.FirstOrDefaultAsync(s => s.Key == key);
        return setting != null && double.TryParse(setting.Value, out var val) ? val : defaultValue;
    }

    public async Task<string> GetStringSettingAsync(string key, string defaultValue)
    {
        var setting = await _context.GameSettings.FirstOrDefaultAsync(s => s.Key == key);
        return setting?.Value ?? defaultValue;
    }
}


