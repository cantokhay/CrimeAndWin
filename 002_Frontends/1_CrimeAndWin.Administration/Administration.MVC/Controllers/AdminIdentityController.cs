using Administration.MVC.ViewModels.IdentityVMs.AppUserVMs;
using Administration.MVC.ViewModels.IdentityVMs.RoleVMs;
using Administration.MVC.ViewModels.IdentityVMs.UserRoleVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;

namespace Administration.MVC.Controllers
{
    public class AdminIdentityController : Controller
    {
        private readonly HttpClient _identityClient;

        public AdminIdentityController(IHttpClientFactory httpClientFactory)
        {
            _identityClient = httpClientFactory.CreateClient("IdentityApi");
        }

        #region App User Management

        // -----------------------
        // APP USER LIST
        // -----------------------
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var result = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers");

            var model = result ?? new List<ResultAppUserVM>();

            return View(model); // Views/AdminIdentity/Users.cshtml
        }

        // -----------------------
        // APP USER CREATE (GET)
        // -----------------------
        [HttpGet]
        public IActionResult CreateUser()
        {
            var vm = new CreateAppUserVM
            {
                EmailConfirmed = true,
                PhoneNumberConfirmed = false
            };

            return View(vm); // Views/AdminIdentity/CreateUser.cshtml
        }

        // -----------------------
        // APP USER CREATE (POST)
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateAppUserVM model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.PasswordHash = model.PasswordHash.ToSHA256String();
            var response = await _identityClient.PostAsJsonAsync("CreateAppUser", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Users));
        }

        // -----------------------
        // APP USER UPDATE (GET)
        // -----------------------
        [HttpGet]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var dto = await _identityClient
                .GetFromJsonAsync<UpdateAppUserVM>($"GetByIdAppUser/{id}");

            if (dto is null)
                return NotFound();

            return View(dto); // Views/AdminIdentity/EditUser.cshtml
        }

        // -----------------------
        // APP USER UPDATE (POST)
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UpdateAppUserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _identityClient.PutAsJsonAsync("UpdateAppUser", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Users));
        }

        // -----------------------
        // APP USER DELETE
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _identityClient.DeleteAsync($"DeleteAppUser/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        #endregion

        #region Role Management

        // -----------------------
        // ROLE LIST
        // -----------------------
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var result = await _identityClient
                .GetFromJsonAsync<List<ResultRoleVM>>("GetAllRoles");

            var model = result ?? new List<ResultRoleVM>();

            return View(model); // Views/AdminIdentity/Roles.cshtml
        }

        // -----------------------
        // ROLE CREATE (GET)
        // -----------------------
        [HttpGet]
        public IActionResult CreateRole()
        {
            var vm = new CreateRoleVM();
            return View(vm); // Views/AdminIdentity/CreateRole.cshtml
        }

        // -----------------------
        // ROLE CREATE (POST)
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new
            {
                Name = model.Name,
                NormalizedName = model.Name.ToUpperInvariant(),
                Description = model.Description
            };

            var response = await _identityClient.PostAsJsonAsync("CreateRole", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Rol oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Roles));
        }

        // -----------------------
        // ROLE UPDATE (GET)
        // -----------------------
        [HttpGet]
        public async Task<IActionResult> EditRole(Guid id)
        {
            var dto = await _identityClient
                .GetFromJsonAsync<UpdateRoleVM>($"GetByIdRole/{id}");

            if (dto is null)
                return NotFound();

            return View(dto); // Views/AdminIdentity/EditRole.cshtml
        }

        // -----------------------
        // ROLE UPDATE (POST)
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(UpdateRoleVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new
            {
                Id = model.Id,
                Name = model.Name,
                NormalizedName = model.Name.ToUpperInvariant(),
                Description = model.Description
            };

            var response = await _identityClient.PutAsJsonAsync("UpdateRole", dto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Rol güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Roles));
        }

        // -----------------------
        // ROLE DELETE
        // -----------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var response = await _identityClient.DeleteAsync($"DeleteRole/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        #endregion

        #region User Role Management

        // =======================
        // USER ROLE LIST
        // =======================
        [HttpGet]
        public async Task<IActionResult> UserRoles()
        {
            // 1) UserRole’leri çek
            var userRoles = await _identityClient
                .GetFromJsonAsync<List<ResultUserRoleVM>>("GetAllUserRoles")
                ?? new List<ResultUserRoleVM>();

            // 2) Kullanıcı ve rolleri çek (dropdown + isim için)
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            var roles = await _identityClient
                .GetFromJsonAsync<List<ResultRoleVM>>("GetAllRoles")
                ?? new List<ResultRoleVM>();

            var userDict = users.ToDictionary(x => x.Id, x => x.UserName);
            var roleDict = roles.ToDictionary(x => x.Id, x => x.Name);

            // 3) Görsel isimleri doldur
            foreach (var ur in userRoles)
            {
                if (userDict.TryGetValue(ur.UserId, out var userName))
                {
                    ur.UserName = userName;
                }

                if (roleDict.TryGetValue(ur.RoleId, out var roleName))
                {
                    ur.RoleName = roleName;
                }
            }

            return View(userRoles); // Views/AdminIdentity/UserRoles.cshtml
        }

        // =======================
        // USER ROLE CREATE (GET)
        // =======================
        [HttpGet]
        public async Task<IActionResult> CreateUserRole()
        {
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            var roles = await _identityClient
                .GetFromJsonAsync<List<ResultRoleVM>>("GetAllRoles")
                ?? new List<ResultRoleVM>();

            var vm = new CreateUserRoleVM
            {
                UserOptions = users
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = $"{u.UserName} ({u.Email})"
                    })
                    .ToList(),
                RoleOptions = roles
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    })
                    .ToList()
            };

            return View(vm); // Views/AdminIdentity/CreateUserRole.cshtml
        }

        // =======================
        // USER ROLE CREATE (POST)
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserRole(CreateUserRoleVM model)
        {
            if (!ModelState.IsValid)
            {
                // dropdown’lar boş kalmasın diye tekrar doldur
                await PopulateUserRoleOptions(model);
                return View(model);
            }

            // API’ye sadece gerekli alanları gönder
            var requestBody = new
            {
                model.UserId,
                model.RoleId
            };

            var response = await _identityClient.PostAsJsonAsync("CreateUserRole", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                await PopulateUserRoleOptions(model);
                ModelState.AddModelError(string.Empty, "Kullanıcıya rol atanırken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(UserRoles));
        }

        // =======================
        // USER ROLE UPDATE (GET)
        // =======================
        [HttpGet]
        public async Task<IActionResult> EditUserRole(Guid id)
        {
            var dto = await _identityClient
                .GetFromJsonAsync<UpdateUserRoleVM>($"GetByIdUserRole/{id}");

            if (dto is null)
                return NotFound();

            await PopulateUserRoleOptions(dto);

            return View(dto); // Views/AdminIdentity/EditUserRole.cshtml
        }

        // =======================
        // USER ROLE UPDATE (POST)
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserRole(UpdateUserRoleVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateUserRoleOptions(model);
                return View(model);
            }

            var requestBody = new
            {
                model.Id,
                model.UserId,
                model.RoleId
            };

            var response = await _identityClient.PutAsJsonAsync("UpdateUserRole", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                await PopulateUserRoleOptions(model);
                ModelState.AddModelError(string.Empty, "Kullanıcı rolü güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(UserRoles));
        }

        // =======================
        // USER ROLE DELETE
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserRole(Guid id)
        {
            var response = await _identityClient.DeleteAsync($"DeleteUserRole/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        // =======================
        // Helper: dropdown doldur
        // =======================
        private async Task PopulateUserRoleOptions(CreateUserRoleVM model)
        {
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            var roles = await _identityClient
                .GetFromJsonAsync<List<ResultRoleVM>>("GetAllRoles")
                ?? new List<ResultRoleVM>();

            model.UserOptions = users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.UserName} ({u.Email})",
                    Selected = (u.Id == model.UserId)
                })
                .ToList();

            model.RoleOptions = roles
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = (r.Id == model.RoleId)
                })
                .ToList();
        }

        private async Task PopulateUserRoleOptions(UpdateUserRoleVM model)
        {
            var users = await _identityClient
                .GetFromJsonAsync<List<ResultAppUserVM>>("GetAllAppUsers")
                ?? new List<ResultAppUserVM>();

            var roles = await _identityClient
                .GetFromJsonAsync<List<ResultRoleVM>>("GetAllRoles")
                ?? new List<ResultRoleVM>();

            model.UserOptions = users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.UserName} ({u.Email})",
                    Selected = (u.Id == model.UserId)
                })
                .ToList();

            model.RoleOptions = roles
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name,
                    Selected = (r.Id == model.RoleId)
                })
                .ToList();
        }

        #endregion
    }
}
