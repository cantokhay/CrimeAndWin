using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CrimeAndWin.Gateway.Controllers;

[Route("api/gateway/admin")]
[ApiController]
public class AdminGatewayController : ControllerBase
{
    [HttpGet("logs")]
    public IActionResult GetRecentLogs([FromQuery] int count = 100)
    {
        var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logDir)) return Ok(new List<object>());

        var logFiles = Directory.GetFiles(logDir, "gateway-*.log")
            .OrderByDescending(f => f)
            .ToList();

        if (logFiles.Count == 0) return Ok(new List<object>());

        var result = new List<GatewayLogEntry>();
        
        foreach (var file in logFiles)
        {
            try 
            {
                var lines = System.IO.File.ReadLines(file).Reverse();
                foreach (var line in lines)
                {
                    var entry = ParseLogLine(line);
                    if (entry != null)
                    {
                        result.Add(entry);
                        if (result.Count >= count) break;
                    }
                }
            } catch { /* Handle file locks if necessary */ }
            
            if (result.Count >= count) break;
        }

        return Ok(result);
    }

    private GatewayLogEntry? ParseLogLine(string line)
    {
        // Example: 2026-03-30 21:39:35.558 +03:00 [INF] Request finished HTTP/1.1 GET https://localhost:7000/api/auth/login - 502 0 null 6901.1431ms
        var regex = new Regex(@"^(?<timestamp>[\d-]+\s[\d:.]+)\s[\+\d:]+\s\[(?<level>\w+)\]\sRequest finished\s(?<proto>\S+)\s(?<method>\S+)\s(?<url>\S+)\s-\s(?<status>\d+)\s(?<len>\d+)\s(?<type>\S+)\s(?<elapsed>[\d.]+)ms");
        var match = regex.Match(line);

        if (match.Success)
        {
            try
            {
                var url = match.Groups["url"].Value;
                var path = url;
                if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
                {
                    path = uri.PathAndQuery;
                }

                return new GatewayLogEntry
                {
                    Timestamp = DateTime.Parse(match.Groups["timestamp"].Value),
                    Method = match.Groups["method"].Value,
                    Path = path,
                    StatusCode = int.Parse(match.Groups["status"].Value),
                    ElapsedMs = double.Parse(match.Groups["elapsed"].Value, System.Globalization.CultureInfo.InvariantCulture),
                    ClientIp = "N/A"
                };
            } catch { return null; }
        }

        return null;
    }

    public class GatewayLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public double ElapsedMs { get; set; }
        public string? ClientIp { get; set; }
    }
}
