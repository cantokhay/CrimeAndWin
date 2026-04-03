using MassTransit;
using Microsoft.EntityFrameworkCore;
using CrimeAndWin.Contracts.Commands.Inventory;
using CrimeAndWin.Contracts.Events.Inventory;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.VOs;

namespace Inventory.API.Consumers
{
    public class GrantItemCommandConsumer : IConsumer<GrantItemCommand>
    {
        private readonly IWriteRepository<Item> _itemWrite;
        private readonly IReadRepository<Inventory.Domain.Entities.Inventory> _invRead;
        private readonly IWriteRepository<Inventory.Domain.Entities.Inventory> _invWrite;
        private readonly IDateTimeProvider _time;

        public GrantItemCommandConsumer(
            IWriteRepository<Item> itemWrite, 
            IReadRepository<Inventory.Domain.Entities.Inventory> invRead,
            IWriteRepository<Inventory.Domain.Entities.Inventory> invWrite,
            IDateTimeProvider time)
        {
            _itemWrite = itemWrite;
            _invRead = invRead;
            _invWrite = invWrite;
            _time = time;
        }

        public async Task Consume(ConsumeContext<GrantItemCommand> context)
        {
            var msg = context.Message;
            try
            {
                var inv = await _invRead.GetWhere(x => x.PlayerId == msg.PlayerId).FirstOrDefaultAsync();
                if (inv == null)
                {
                    inv = new Inventory.Domain.Entities.Inventory
                    {
                        PlayerId = msg.PlayerId,
                        //Capacity = 100,
                        CreatedAtUtc = _time.UtcNow
                    };
                    await _invWrite.AddAsync(inv);
                    await _invWrite.SaveAsync();
                }

                var item = new Item
                {
                    Id = msg.ItemId,
                    InventoryId = inv.Id,
                    Name = $"Reward Item {msg.ItemId.ToString().Substring(0, 4)}",
                    Quantity = msg.Quantity,
                    Stats = new ItemStats(0, 0, 0),
                    //Value = new ItemValue(0m, CurrencyType.Cash),
                    CreatedAtUtc = _time.UtcNow
                };

                await _itemWrite.AddAsync(item);
                var saveResult = await _itemWrite.SaveAsync();

                await context.Publish(new InventoryGrantedEvent
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = saveResult > 0,
                    FailReason = saveResult > 0 ? null : "Could not save item to database."
                });
            }
            catch (Exception ex)
            {
                await context.Publish(new InventoryGrantedEvent
                {
                    CorrelationId = msg.CorrelationId,
                    PlayerId = msg.PlayerId,
                    IsSuccess = false,
                    FailReason = ex.Message
                });
            }
        }
    }
}


