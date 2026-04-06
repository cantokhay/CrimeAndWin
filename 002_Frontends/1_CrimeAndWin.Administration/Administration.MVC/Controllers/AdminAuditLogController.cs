using Administration.MVC.ViewModels.AuditVMs;
using Microsoft.AspNetCore.Mvc;

namespace Administration.MVC.Controllers
{
    public class AdminAuditLogController : BaseAdminController
    {
        private readonly HttpClient _auditClient;

        public AdminAuditLogController(IHttpClientFactory httpClientFactory)
        {
            _auditClient = httpClientFactory.CreateClient("IdentityApi"); 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // MOCK DATA for showcase/UI fix since backend endpoint is missing
            var logs = new List<AdminAuditLogVM>
            {
                new AdminAuditLogVM {
                    Id = Guid.NewGuid(),
                    AdminUser = "SuperAdmin",
                    ActionType = "UPDATE",
                    EntityName = "User",
                    EntityId = Guid.NewGuid().ToString(),
                    Description = "User roles were modified for security",
                    OldValue = "{\"Roles\": [\"Player\"]}",
                    NewValue = "{\"Roles\": [\"Player\", \"Moderator\"]}",
                    IpAddress = "192.168.1.100",
                    Timestamp = DateTime.Now.AddHours(-2),
                    IsSuccess = true
                },
                new AdminAuditLogVM {
                    Id = Guid.NewGuid(),
                    AdminUser = "SuperAdmin",
                    ActionType = "DELETE",
                    EntityName = "Player",
                    EntityId = Guid.NewGuid().ToString(),
                    Description = "Cheating violation - Permanent ban",
                    OldValue = "{\"Status\": \"Active\"}",
                    NewValue = "{\"Status\": \"Banned\"}",
                    IpAddress = "192.168.1.100",
                    Timestamp = DateTime.Now.AddHours(-5),
                    IsSuccess = true
                },
                new AdminAuditLogVM {
                    Id = Guid.NewGuid(),
                    AdminUser = "System",
                    ActionType = "SYSTEM",
                    EntityName = "GlobalSettings",
                    EntityId = "Global",
                    Description = "Night mode cycle started",
                    OldValue = "{\"DayTime\": true}",
                    NewValue = "{\"DayTime\": false}",
                    IpAddress = "127.0.0.1",
                    Timestamp = DateTime.Now.AddDays(-1),
                    IsSuccess = true
                }
            };

            return View(logs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var log = await _auditClient
                .GetFromJsonAsync<AdminAuditLogVM>($"GetAuditLogById/{id}");

            if (log == null) return NotFound();

            return View(log);
        }
    }
}
