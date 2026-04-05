using Administration.MVC.Services.Dtos;
using System.Net.Http.Json;

namespace Administration.MVC.Services;

public class GatewayApiClient(HttpClient http)
{
    public async Task<List<GatewayLogDto>> GetRecentLogsAsync(int count = 100)
    {
        try { return (await http.GetFromJsonAsync<List<GatewayLogDto>>($"api/gateway/admin/logs?count={count}")) ?? []; }
        catch { return []; }
    }
}
