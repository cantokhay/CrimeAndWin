using Inventory.Application.Mapping;
using Inventory.Application.DTOs.InventoryDTOs;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventories
{
    public sealed class GetAllInventoriesHandler(
            IReadRepository<Domain.Entities.Inventory> readRepo,
            InventoryMapper mapper)
            : IRequestHandler<GetAllInventoriesQuery, List<ResultInventoryDTO>>
    {
        public async Task<List<ResultInventoryDTO>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            var inventories = await readRepo.Table
                .Include(x => x.Items)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(inventories).ToList();
        }
    }
}



