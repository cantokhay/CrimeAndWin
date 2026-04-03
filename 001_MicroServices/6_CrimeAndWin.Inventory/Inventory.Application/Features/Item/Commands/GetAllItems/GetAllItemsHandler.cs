using Inventory.Application.Mapping;
using Inventory.Application.DTOs.ItemDTOs;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Item.Commands.GetAllItems
{
    public sealed class GetAllItemsHandler(
            IReadRepository<Domain.Entities.Item> readRepo,
            InventoryMapper mapper)
            : IRequestHandler<GetAllItemsQuery, List<ResultItemDTO>>
    {
        public async Task<List<ResultItemDTO>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await readRepo.Table
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(items).ToList();
        }
    }
}



