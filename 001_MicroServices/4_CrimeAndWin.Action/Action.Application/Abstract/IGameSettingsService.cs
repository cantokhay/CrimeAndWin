using System.Threading.Tasks;

namespace Action.Application.Abstract;

public interface IGameSettingsService
{
    Task<int> GetIntSettingAsync(string key, int defaultValue);
    Task<double> GetDoubleSettingAsync(string key, double defaultValue);
    Task<string> GetStringSettingAsync(string key, string defaultValue);
}


