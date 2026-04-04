using Administration.MVC.Services.Dtos;
using Microsoft.Extensions.Configuration;

namespace Administration.MVC.Services;

public class HealthApiClient(HttpClient http, IConfiguration config)
{
    public async Task<List<ServiceHealthDto>> GetAllHealthAsync()
    {
        var services = new[]
        {
            ("Identity",      "http://localhost:6001"),
            ("PlayerProfile", "http://localhost:6002"),
            ("GameWorld",     "http://localhost:6003"),
            ("Action",        "http://localhost:6004"),
            ("Economy",       "http://localhost:6005"),
            ("Inventory",     "http://localhost:6006"),
            ("Leadership",    "http://localhost:6007"),
            ("Notification",  "http://localhost:6008"),
            ("Moderation",    "http://localhost:6009"),
            ("Saga",          "http://localhost:6910"),
            ("Gateway",       "http://localhost:5000"),
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
