using Administration.MVC.Services.Dtos;
using Microsoft.Extensions.Configuration;

namespace Administration.MVC.Services;

public class HealthApiClient(HttpClient http, IConfiguration config)
{
    public async Task<List<ServiceHealthDto>> GetAllHealthAsync()
    {
        var services = new[]
        {
            ("Identity",      "https://localhost:6101"),
            ("PlayerProfile", "https://localhost:6102"),
            ("GameWorld",     "https://localhost:6103"),
            ("Action",        "https://localhost:6104"),
            ("Economy",       "https://localhost:6105"),
            ("Inventory",     "https://localhost:6106"),
            ("Leadership",    "https://localhost:6107"),
            ("Notification",  "https://localhost:6108"),
            ("Moderation",    "https://localhost:6109"),
            ("Saga",          "https://localhost:6110"),
            ("Gateway",       "https://localhost:7000"),
        };

        var tasks = services.Select(async s =>
        {
            try
            {
                var response = await http.GetAsync($"{s.Item2}/health");
                return new ServiceHealthDto
                {
                    ServiceName = s.Item1,
                    IsHealthy   = response.IsSuccessStatusCode,
                    StatusCode  = (int)response.StatusCode,
                    CheckedAt   = DateTime.UtcNow,
                };
            }
            catch (Exception ex)
            {
                return new ServiceHealthDto
                {
                    ServiceName  = s.Item1,
                    IsHealthy    = false,
                    StatusCode   = 0,
                    ErrorMessage = ex.Message,
                    CheckedAt    = DateTime.UtcNow,
                };
            }
        });

        return (await Task.WhenAll(tasks)).ToList();
    }
}
