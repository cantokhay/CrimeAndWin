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
            // Assuming an endpoint exists or we're mocking for the UI showcase
            var logs = await _auditClient
                .GetFromJsonAsync<List<AdminAuditLogVM>>("GetAdminAuditLogs")
                ?? new List<AdminAuditLogVM>();

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
