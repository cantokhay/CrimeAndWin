using Inventory.Domain.Enums;
using Inventory.Domain.VOs;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Features.Inventory.Commands.Seed
{
    public sealed class RunInventorySeedHandler : IRequestHandler<RunInventorySeedCommand, Unit>
    {
        private readonly IWriteRepository<global::Inventory.Domain.Entities.Inventory> _inventoryWrite;
        private readonly IReadRepository<global::Inventory.Domain.Entities.Inventory> _inventoryRead;
        private readonly IWriteRepository<global::Inventory.Domain.Entities.Item> _itemWrite;
        private readonly IReadRepository<global::Inventory.Domain.Entities.Item> _itemRead;

        public RunInventorySeedHandler(
            IWriteRepository<global::Inventory.Domain.Entities.Inventory> inventoryWrite,
            IReadRepository<global::Inventory.Domain.Entities.Inventory> inventoryRead,
            IWriteRepository<global::Inventory.Domain.Entities.Item> itemWrite,
            IReadRepository<global::Inventory.Domain.Entities.Item> itemRead)
        {
            _inventoryWrite = inventoryWrite;
            _inventoryRead = inventoryRead;
            _itemWrite = itemWrite;
            _itemRead = itemRead;
        }

        public async Task<Unit> Handle(RunInventorySeedCommand request, CancellationToken ct)
        {
            var seedDate = SeedDataConstants.SeedDate;

            // Core Inventories
            var coreInventories = new List<global::Inventory.Domain.Entities.Inventory>
            {
                new()
                {
                    Id = SeedDataConstants.InventoryAlphaId,
                    PlayerId = SeedDataConstants.PlayerAlphaId,
                    CreatedAtUtc = seedDate
                },
                new()
                {
                    Id = SeedDataConstants.InventoryBetaId,
                    PlayerId = SeedDataConstants.PlayerBetaId,
                    CreatedAtUtc = seedDate
                }
            };

            foreach (var inv in coreInventories)
            {
                if (await _inventoryRead.GetByIdAsync(inv.Id.ToString()) == null)
                {
                    await _inventoryWrite.AddAsync(inv);
                }
            }
            await _inventoryWrite.SaveAsync();

            // Core Items
            var coreItems = new List<global::Inventory.Domain.Entities.Item>
            {
                new()
                {
                    Id = SeedDataConstants.ItemDesertEagleId,
                    InventoryId = SeedDataConstants.InventoryAlphaId,
                    Name = "Desert Eagle",
                    Quantity = 1,
                    Stats = new ItemStats(Damage: 50, Defense: 0, Power: 0),
                    Value = new ItemValue(Amount: 1500, Currency: CurrencyType.Credit),
                    CreatedAtUtc = seedDate
                },
                new()
                {
                    Id = SeedDataConstants.ItemKevlarVestId,
                    InventoryId = SeedDataConstants.InventoryAlphaId,
                    Name = "Kevlar Vest",
                    Quantity = 1,
                    Stats = new ItemStats(Damage: 0, Defense: 40, Power: 0),
                    Value = new ItemValue(Amount: 2500, Currency: CurrencyType.Credit),
                    CreatedAtUtc = seedDate
                },
                new()
                {
                    Id = SeedDataConstants.ItemAdrenalineId,
                    InventoryId = SeedDataConstants.InventoryBetaId,
                    Name = "Adrenaline Shot",
                    Quantity = 1,
                    Stats = new ItemStats(Damage: 0, Defense: 0, Power: 20),
                    Value = new ItemValue(Amount: 500, Currency: CurrencyType.Credit),
                    CreatedAtUtc = seedDate
                }
            };

            foreach (var item in coreItems)
            {
                if (await _itemRead.GetByIdAsync(item.Id.ToString()) == null)
                {
                    await _itemWrite.AddAsync(item);
                }
            }
            await _itemWrite.SaveAsync();

            return Unit.Value;
        }
    }
}
