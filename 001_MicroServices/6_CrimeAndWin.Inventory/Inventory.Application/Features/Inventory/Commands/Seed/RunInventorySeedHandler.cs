using Bogus;
using Inventory.Domain.Enums;
using Inventory.Domain.VOs;
using MediatR;
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

        public async Task<Unit> Handle(RunInventorySeedCommand request, CancellationToken cancellationToken)
        {
            var faker = new Faker("en");

            var inventories = new List<Domain.Entities.Inventory>();
            var items = new List<Domain.Entities.Item>();

            for (int i = 0; i < request.Count; i++)
            {
                var invId = Guid.NewGuid();
                var inv = new Domain.Entities.Inventory
                {
                    Id = invId,
                    PlayerId = Guid.NewGuid(), // fake PlayerId
                    CreatedAtUtc = _clock.UtcNow,
                    IsDeleted = false,
                    Items = new List<Domain.Entities.Item>()
                };

                // Her envantere 3-6 rastgele item
                var itemCount = faker.Random.Int(3, 6);
                for (int j = 0; j < itemCount; j++)
                {
                    var item = new Domain.Entities.Item
                    {
                        Id = Guid.NewGuid(),
                        InventoryId = invId,
                        Name = faker.Commerce.ProductName(),
                        Quantity = faker.Random.Int(1, 5),
                        Stats = new ItemStats(
                            Damage: faker.Random.Int(5, 100),
                            Defense: faker.Random.Int(5, 100),
                            Power: faker.Random.Int(5, 100)
                        ),
                        Value = new ItemValue(
                            Amount: faker.Random.Decimal(10, 500),
                            Currency: faker.PickRandom<CurrencyType>()
                        ),
                        CreatedAtUtc = _clock.UtcNow,
                        IsDeleted = false
                    };

                    inv.Items.Add(item);
                    items.Add(item);
                }

                inventories.Add(inv);
            }

            await _inventoryRepo.AddRangeAsync(inventories);
            await _itemRepo.AddRangeAsync(items);

            await _inventoryRepo.SaveAsync();
            await _itemRepo.SaveAsync();

            return Unit.Value;
        }
    }
}
