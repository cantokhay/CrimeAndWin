using AutoMapper;
using Inventory.Application.DTOs.InventoryDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Inventory.Queries.GetAllInventories
{
    public sealed class GetAllInventoriesHandler(
            IReadRepository<Domain.Entities.Inventory> readRepo,
            IMapper mapper)
            : IRequestHandler<GetAllInventoriesQuery, List<ResultInventoryDTO>>
    {
        public async Task<List<ResultInventoryDTO>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            var inventories = await readRepo.Table
                .Include(x => x.Items)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return mapper.Map<List<ResultInventoryDTO>>(inventories);
        }
    }
}
