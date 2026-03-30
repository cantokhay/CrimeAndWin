using Administration.MVC.ViewModels.InventoryVMs.InventoryVMs;
using Administration.MVC.ViewModels.InventoryVMs.ItemVMs;
using Administration.MVC.ViewModels.InventoryVMs.PlayerInventoryItemsVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminInventoryController : Controller
    {
        private readonly HttpClient _inventoryClient;
        private readonly HttpClient _playerClient;

        public AdminInventoryController(IHttpClientFactory httpClientFactory)
        {
            _inventoryClient = httpClientFactory.CreateClient("InventoryApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
        }

        #region Inventory Operations

        // ============================
        // INVENTORY LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Inventories()
        {
            var inventoriesDto = await _inventoryClient
                .GetFromJsonAsync<List<InventoryDtoProxy>>("GetAllInventoriesAsAdmin")
                ?? new List<InventoryDtoProxy>();

            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            var pDict = players.ToDictionary(x => x.Id, x => x);

            var model = inventoriesDto.Select(x =>
            {
                var vm = new ResultInventoryVM
                {
                    Id = x.Id,
                    PlayerId = x.PlayerId,
                    ItemsCount = x.Items?.Count ?? 0,
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                };

                if (pDict.TryGetValue(vm.PlayerId, out var p))
                {
                    vm.PlayerDisplay = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})";
                }

                return vm;
            }).ToList();

            return View(model); // Views/AdminInventory/Inventories.cshtml
        }

        // ============================
        // INVENTORY CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateInventory()
        {
            var vm = new CreateInventoryVM();
            await PopulatePlayerOptions(vm);
            return View(vm);
        }

        // ============================
        // INVENTORY CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInventory(CreateInventoryVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model);
                return View(model);
            }

            var response = await _inventoryClient.PostAsJsonAsync("CreateInventoryAsAdmin", new
            {
                model.PlayerId
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model);
                ModelState.AddModelError(string.Empty, "Inventory oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Inventories));
        }

        // ============================
        // INVENTORY UPDATE (GET)
        // ============================
        // ============================
        // INVENTORY UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditInventory(Guid id)
        {
            var inventories = await _inventoryClient
                .GetFromJsonAsync<List<InventoryDtoProxy>>("GetAllInventoriesAsAdmin")
                ?? new List<InventoryDtoProxy>();

            var inv = inventories.FirstOrDefault(x => x.Id == id);
            if (inv is null)
                return NotFound();

            var vm = new UpdateInventoryVM
            {
                Id = inv.Id,
                PlayerId = inv.PlayerId
            };

            await PopulatePlayerOptions(vm);
            return View(vm); // Views/AdminInventory/EditInventory.cshtml
        }

        // ============================
        // INVENTORY UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInventory(UpdateInventoryVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model);
                return View(model);
            }

            var response = await _inventoryClient.PutAsJsonAsync("UpdateInventoryAsAdmin", new
            {
                model.Id,
                model.PlayerId
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model);
                ModelState.AddModelError(string.Empty, "Inventory güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Inventories));
        }

        // ============================
        // INVENTORY DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInventory(Guid id)
        {
            var response = await _inventoryClient.DeleteAsync($"DeleteInventoryAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        #endregion

        #region Item Operations

        // ============================
        // ITEM LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Items()
        {
            var items = await _inventoryClient
                .GetFromJsonAsync<List<ResultItemVM>>("GetAllItemsAsAdmin")
                ?? new List<ResultItemVM>();

            var inventories = await _inventoryClient
                .GetFromJsonAsync<List<InventoryDtoProxy>>("GetAllInventoriesAsAdmin")
                ?? new List<InventoryDtoProxy>();

            var invDict = inventories.ToDictionary(x => x.Id, x => x);

            foreach (var it in items)
            {
                it.CurrencyText = it.Currency.ToString();

                if (invDict.TryGetValue(it.InventoryId, out var inv))
                {
                    it.InventoryDisplay = $"{inv.Id} | Items: {(inv.Items?.Count ?? 0)}";
                }
            }

            return View(items); // Views/AdminInventory/Items.cshtml
        }

        // ============================
        // ITEM CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateItem(Guid? inventoryId)
        {
            var vm = new CreateItemVM
            {
                Quantity = 1
            };

            await PopulateInventoryOptions(vm);
            PopulateCurrencyOptions(vm);

            if (inventoryId.HasValue)
                vm.InventoryId = inventoryId.Value;

            foreach (var opt in vm.InventoryOptions)
                opt.Selected = (opt.Value == vm.InventoryId.ToString());

            return View(vm);
        }

        // ============================
        // ITEM CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(CreateItemVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateInventoryOptions(model);
                PopulateCurrencyOptions(model);
                return View(model);
            }

            var response = await _inventoryClient.PostAsJsonAsync("CreateItemAsAdmin", new
            {
                model.InventoryId,
                model.Name,
                model.Quantity,
                model.Damage,
                model.Defense,
                model.Power,
                model.Amount,
                Currency = model.Currency
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateInventoryOptions(model);
                PopulateCurrencyOptions(model);
                ModelState.AddModelError(string.Empty, "Item oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Items));
        }

        // ============================
        // ITEM UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditItem(Guid id)
        {
            var dto = await _inventoryClient
                .GetFromJsonAsync<UpdateItemVM>($"GetItemByIdAsAdmin/{id}");

            if (dto is null)
                return NotFound();

            await PopulateInventoryOptions(dto);
            PopulateCurrencyOptions(dto);

            return View(dto);
        }

        // ============================
        // ITEM UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(UpdateItemVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateInventoryOptions(model);
                PopulateCurrencyOptions(model);
                return View(model);
            }

            var response = await _inventoryClient.PutAsJsonAsync("UpdateItemAsAdmin", new
            {
                model.Id,
                model.InventoryId,
                model.Name,
                model.Quantity,
                model.Damage,
                model.Defense,
                model.Power,
                model.Amount,
                Currency = model.Currency
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateInventoryOptions(model);
                PopulateCurrencyOptions(model);
                ModelState.AddModelError(string.Empty, "Item güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Items));
        }

        // ============================
        // ITEM DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var response = await _inventoryClient.DeleteAsync($"DeleteItemAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        // ============================
        // PLAYER INVENTORY ITEMS
        // ============================
        [HttpGet]
        public async Task<IActionResult> PlayerInventoryItems()
        {
            var vm = new PlayerInventoryItemsVM();

            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            vm.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})"
                })
                .ToList();

            vm.InventoryOptions = new List<SelectListItem>();

            return View(vm); // Views/AdminInventory/PlayerInventoryItems.cshtml
        }

        // GET: /AdminInventory/GetInventoriesByPlayer?playerId=...
        [HttpGet]
        public async Task<IActionResult> GetInventoriesByPlayer(Guid playerId)
        {
            var inventories = await _inventoryClient
                .GetFromJsonAsync<List<InventoryDtoProxy>>("GetAllInventoriesAsAdmin")
                ?? new List<InventoryDtoProxy>();

            var list = inventories
                .Where(i => i.PlayerId == playerId)
                .Select(i => new
                {
                    id = i.Id,
                    text = $"{i.Id} | Items: {(i.Items?.Count ?? 0)}"
                })
                .ToList();

            return Json(list);
        }

        // GET: /AdminInventory/GetItemsByInventory?inventoryId=...
        [HttpGet]
        public async Task<IActionResult> GetItemsByInventory(Guid inventoryId)
        {
            var allItems = await _inventoryClient
                .GetFromJsonAsync<List<ResultItemVM>>("GetAllItemsAsAdmin")
                ?? new List<ResultItemVM>();

            var items = allItems
                .Where(x => x.InventoryId == inventoryId)
                .ToList();

            foreach (var it in items)
            {
                it.InventoryDisplay = inventoryId.ToString();
                it.CurrencyText = it.Currency.ToString();
            }

            return Json(items);
        }

        [HttpGet]
        public IActionResult CreateItemForInventory(Guid inventoryId)
        {
            return RedirectToAction("CreateItem", new { inventoryId });
        }

        #endregion

        // ============================
        // HELPERS
        // ============================
        private async Task PopulatePlayerOptions(CreateInventoryVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                    Selected = p.Id == model.PlayerId
                })
                .ToList();
        }

        private async Task PopulatePlayerOptions(UpdateInventoryVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                    Selected = p.Id == model.PlayerId
                })
                .ToList();
        }

        private async Task PopulateInventoryOptions(CreateItemVM model)
        {
            var inventories = await _inventoryClient
                .GetFromJsonAsync<List<InventoryDtoProxy>>("GetAllInventoriesAsAdmin")
                ?? new List<InventoryDtoProxy>();

            model.InventoryOptions = inventories
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Id} | Player: {i.PlayerId} | Items: {(i.Items?.Count ?? 0)}",
                    Selected = (i.Id == model.InventoryId)
                })
                .ToList();
        }

        private async Task PopulateInventoryOptions(UpdateItemVM model)
        {
            var inventories = await _inventoryClient
                .GetFromJsonAsync<List<InventoryDtoProxy>>("GetAllInventoriesAsAdmin")
                ?? new List<InventoryDtoProxy>();

            model.InventoryOptions = inventories
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Id} | Player: {i.PlayerId} | Items: {(i.Items?.Count ?? 0)}",
                    Selected = (i.Id == model.InventoryId)
                })
                .ToList();
        }

        private void PopulateCurrencyOptions(CreateItemVM model)
        {
            model.CurrencyOptions = new List<SelectListItem>
            {
                new("Gold (1)", "1", model.Currency == 1),
                new("Gem (2)", "2", model.Currency == 2),
                new("Credit (3)", "3", model.Currency == 3),
            };
        }

        private void PopulateCurrencyOptions(UpdateItemVM model)
        {
            model.CurrencyOptions = new List<SelectListItem>
            {
                new("Gold (1)", "1", model.Currency == 1),
                new("Gem (2)", "2", model.Currency == 2),
                new("Credit (3)", "3", model.Currency == 3),
            };
        }

        // ============================
        // API DTO PROXIES
        // ============================
        private sealed class InventoryDtoProxy
        {
            public Guid Id { get; set; }
            public Guid PlayerId { get; set; }
            public List<ItemDtoProxy>? Items { get; set; }
            public DateTime CreatedAtUtc { get; set; }
            public DateTime? UpdatedAtUtc { get; set; }
        }

        private sealed class ItemDtoProxy
        {
            public Guid Id { get; set; }
        }

        private sealed class InventoryWithItemsProxy
        {
            public Guid Id { get; set; }
            public Guid PlayerId { get; set; }
            public List<ResultItemVM> Items { get; set; } = new();
        }

    }
}
