using Administration.MVC.ViewModels.EconomyVMs.TransactionVMs;
using Administration.MVC.ViewModels.EconomyVMs.WalletVMs;
using Administration.MVC.ViewModels.PlayerProfileVMs.Lookups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.Controllers
{
    public class AdminEconomyController : Controller
    {
        private readonly HttpClient _economyClient;
        private readonly HttpClient _playerClient;

        public AdminEconomyController(IHttpClientFactory httpClientFactory)
        {
            _economyClient = httpClientFactory.CreateClient("EconomyApi");
            _playerClient = httpClientFactory.CreateClient("PlayerProfileApi");
        }

        #region Wallet Operations

        // ============================
        // WALLET LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Wallets()
        {
            var wallets = await _economyClient
                .GetFromJsonAsync<List<ResultWalletVM>>("GetAllWalletsAsAdmin")
                ?? new List<ResultWalletVM>();

            // PlayerId -> Display text
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            var dict = players.ToDictionary(p => p.Id, p => p);

            foreach (var w in wallets)
            {
                if (dict.TryGetValue(w.PlayerId, out var p))
                {
                    w.PlayerDisplay = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})";
                }
            }

            return View(wallets); // Views/AdminEconomy/Wallets.cshtml
        }

        // ============================
        // WALLET CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateWallet()
        {
            var vm = new CreateWalletVM
            {
                Balance = 0
            };

            await PopulatePlayerOptions(vm);
            return View(vm); // Views/AdminEconomy/CreateWallet.cshtml
        }

        // ============================
        // WALLET CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWallet(CreateWalletVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model);
                return View(model);
            }

            var response = await _economyClient.PostAsJsonAsync("CreateWalletAsAdmin", new
            {
                model.PlayerId,
                model.Balance
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model);
                ModelState.AddModelError(string.Empty, "Wallet oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Wallets));
        }

        // ============================
        // WALLET UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditWallet(Guid id)
        {
            var dto = await _economyClient
                .GetFromJsonAsync<UpdateWalletVM>($"GetWalletByIdAsAdmin/{id}");

            if (dto is null)
                return NotFound();

            await PopulatePlayerOptions(dto);
            return View(dto); // Views/AdminEconomy/EditWallet.cshtml
        }

        // ============================
        // WALLET UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWallet(UpdateWalletVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulatePlayerOptions(model);
                return View(model);
            }

            var response = await _economyClient.PutAsJsonAsync("UpdateWalletAsAdmin", new
            {
                model.Id,
                model.PlayerId,
                model.Balance
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulatePlayerOptions(model);
                ModelState.AddModelError(string.Empty, "Wallet güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Wallets));
        }

        // ============================
        // WALLET DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var response = await _economyClient.DeleteAsync($"DeleteWalletAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }


        #endregion


        #region Transaction Operations

        // ============================
        // TRANSACTION LIST
        // ============================
        [HttpGet]
        public async Task<IActionResult> Transactions()
        {
            var txs = await _economyClient
                .GetFromJsonAsync<List<ResultTransactionVM>>("GetAllTransactionsAsAdmin")
                ?? new List<ResultTransactionVM>();

            // WalletId -> Display
            var wallets = await _economyClient
                .GetFromJsonAsync<List<ResultWalletVM>>("GetAllWalletsAsAdmin")
                ?? new List<ResultWalletVM>();

            var wdict = wallets.ToDictionary(w => w.Id, w => w);

            foreach (var t in txs)
            {
                if (wdict.TryGetValue(t.WalletId, out var w))
                {
                    t.WalletDisplay = $"{w.Id} | Balance: {w.Balance}";
                }
            }

            return View(txs); // Views/AdminEconomy/Transactions.cshtml
        }

        // ============================
        // TRANSACTION CREATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> CreateTransaction()
        {
            var vm = new CreateTransactionVM
            {
                Amount = 0,
                CurrencyType = "Cash",
                ReasonCode = "Admin",
                Description = ""
            };

            await PopulateWalletOptions(vm);
            return View(vm); // Views/AdminEconomy/CreateTransaction.cshtml
        }

        // ============================
        // TRANSACTION CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransaction(CreateTransactionVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateWalletOptions(model);
                return View(model);
            }

            var response = await _economyClient.PostAsJsonAsync("CreateTransactionAsAdmin", new
            {
                model.WalletId,
                model.Amount,
                model.CurrencyType,
                model.ReasonCode,
                model.Description
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateWalletOptions(model);
                ModelState.AddModelError(string.Empty, "Transaction oluşturulurken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Transactions));
        }

        // ============================
        // TRANSACTION UPDATE (GET)
        // ============================
        [HttpGet]
        public async Task<IActionResult> EditTransaction(Guid id)
        {
            var dto = await _economyClient
                .GetFromJsonAsync<UpdateTransactionVM>($"GetTransactionByIdAsAdmin/{id}");

            if (dto is null)
                return NotFound();

            await PopulateWalletOptions(dto);
            return View(dto); // Views/AdminEconomy/EditTransaction.cshtml
        }

        // ============================
        // TRANSACTION UPDATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTransaction(UpdateTransactionVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateWalletOptions(model);
                return View(model);
            }

            var response = await _economyClient.PutAsJsonAsync("UpdateTransactionAsAdmin", new
            {
                model.Id,
                model.WalletId,
                model.Amount,
                model.CurrencyType,
                model.ReasonCode,
                model.Description
            });

            if (!response.IsSuccessStatusCode)
            {
                await PopulateWalletOptions(model);
                ModelState.AddModelError(string.Empty, "Transaction güncellenirken bir hata oluştu.");
                return View(model);
            }

            return RedirectToAction(nameof(Transactions));
        }

        // ============================
        // TRANSACTION DELETE
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var response = await _economyClient.DeleteAsync($"DeleteTransactionAsAdmin/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }

        #endregion

        // ============================
        // HELPERS
        // ============================
        private async Task PopulatePlayerOptions(CreateWalletVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                    Selected = (p.Id == model.PlayerId)
                })
                .ToList();
        }

        private async Task PopulatePlayerOptions(UpdateWalletVM model)
        {
            var players = await _playerClient
                .GetFromJsonAsync<List<PlayerLookupVM>>("GetAllPlayersAsAdmin")
                ?? new List<PlayerLookupVM>();

            model.PlayerOptions = players
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{(string.IsNullOrWhiteSpace(p.DisplayName) ? "Player" : p.DisplayName)} ({p.Id})",
                    Selected = (p.Id == model.PlayerId)
                })
                .ToList();
        }

        private async Task PopulateWalletOptions(CreateTransactionVM model)
        {
            var wallets = await _economyClient
                .GetFromJsonAsync<List<ResultWalletVM>>("GetAllWalletsAsAdmin")
                ?? new List<ResultWalletVM>();

            model.WalletOptions = wallets
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = $"{w.Id} | Balance: {w.Balance}",
                    Selected = (w.Id == model.WalletId)
                })
                .ToList();
        }

        private async Task PopulateWalletOptions(UpdateTransactionVM model)
        {
            var wallets = await _economyClient
                .GetFromJsonAsync<List<ResultWalletVM>>("GetAllWalletsAsAdmin")
                ?? new List<ResultWalletVM>();

            model.WalletOptions = wallets
                .Select(w => new SelectListItem
                {
                    Value = w.Id.ToString(),
                    Text = $"{w.Id} | Balance: {w.Balance}",
                    Selected = (w.Id == model.WalletId)
                })
                .ToList();
        }
    }
}
