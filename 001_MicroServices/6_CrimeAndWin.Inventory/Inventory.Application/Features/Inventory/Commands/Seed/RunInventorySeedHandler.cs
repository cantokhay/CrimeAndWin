using Inventory.Domain.Enums;
using Inventory.Domain.VOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Inventory.Application.Features.Inventory.Commands.Seed
{
    public sealed class RunInventorySeedHandler : IRequestHandler<RunInventorySeedCommand, Unit>
    {
        private readonly IWriteRepository<Domain.Entities.Inventory> _inventoryRepo;
        private readonly IWriteRepository<Domain.Entities.Item> _itemRepo;
        private readonly IDateTimeProvider _clock;

        public RunInventorySeedHandler(
            IWriteRepository<Domain.Entities.Inventory> inventoryRepo,
            IWriteRepository<Domain.Entities.Item> itemRepo,
            IDateTimeProvider clock)
        {
            _inventoryRepo = inventoryRepo;
            _itemRepo = itemRepo;
            _clock = clock;
        }

        public async Task<Unit> Handle(RunInventorySeedCommand request, CancellationToken ct)
        {
            var now = _clock.UtcNow;
            var themeUsers = new[] { "Boss", "Hitman", "Mole", "Fixer", "Dealer", "Enforcer", "Launderer" };
            
            var inventories = new List<Domain.Entities.Inventory>();
            var items = new List<Domain.Entities.Item>();

            var crimeItems = new List<(string Name, int Dmg, int Def, int Pwr, decimal Value)>
            {
                ("Lockpick Set", 0, 0, 20, 150.0m),
                ("Crowbar", 10, 5, 10, 50.0m),
                ("9mm Pistol", 50, 0, 0, 1500.0m),
                ("Kevlar Vest", 0, 40, 0, 2500.0m),
                ("Brass Knuckles", 15, 0, 0, 150.0m),
                ("Silenced SMG", 70, 0, 0, 4500.0m),
                ("Encrypted Laptop", 0, 0, 50, 3500.0m)
            };

            for (int i = 0; i < themeUsers.Length; i++)
            {
                var invId = Guid.Parse($"55555555-5555-5555-5555-{i:D12}");
                var playerId = Guid.Parse($"22222222-2222-2222-2222-{i:D12}"); // From PlayerProfile
                
                inventories.Add(new Domain.Entities.Inventory
                {
                    Id = invId,
                    PlayerId = playerId,
                    CreatedAtUtc = now
                });

                // Give player 2 random crime items
                for (int j = 0; j < 2; j++)
                {
                    var cItem = crimeItems[(i + j) % crimeItems.Count];
                    items.Add(new Domain.Entities.Item
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = invId,
                        Name = cItem.Name,
                        Quantity = 1,
                        Stats = new ItemStats(Damage: cItem.Dmg, Defense: cItem.Def, Power: cItem.Pwr),
                        Value = new ItemValue(Amount: cItem.Value, Currency: CurrencyType.CASH),
                        CreatedAtUtc = now
                    });
                }
            }

            try {
                await _inventoryRepo.AddRangeAsync(inventories);
                await _itemRepo.AddRangeAsync(items);
                await _inventoryRepo.SaveAsync();
                await _itemRepo.SaveAsync();
            } catch { }

            return Unit.Value;
        }
    }
}
