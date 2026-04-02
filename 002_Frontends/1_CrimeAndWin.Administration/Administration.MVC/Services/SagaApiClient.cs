using Administration.MVC.Services.Dtos;
using System.Net.Http.Json;

namespace Administration.MVC.Services;

public class SagaApiClient(HttpClient http)
{
    public async Task<List<SagaStateDto>> GetAllSagaStatesAsync(string? stateFilter = null)
    {
        var url = stateFilter is null ? "/api/saga/admin/states" : $"/api/saga/admin/states?state={stateFilter}";
        try { return (await http.GetFromJsonAsync<List<SagaStateDto>>(url)) ?? []; }
        catch { return []; }
    }

    public async Task<SagaDetailDto?> GetSagaDetailAsync(Guid correlationId)
    {
        try { return await http.GetFromJsonAsync<SagaDetailDto>($"/api/saga/admin/states/{correlationId}"); }
        catch { return null; }
    }
}
