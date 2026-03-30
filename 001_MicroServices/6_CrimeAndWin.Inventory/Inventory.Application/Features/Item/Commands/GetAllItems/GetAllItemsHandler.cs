using AutoMapper;
using Inventory.Application.DTOs.ItemDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Item.Commands.GetAllItems
{
    public sealed class GetAllItemsHandler(
            IReadRepository<Domain.Entities.Item> readRepo,
            IMapper mapper)
            : IRequestHandler<GetAllItemsQuery, List<ResultItemDTO>>
    {
        public async Task<List<ResultItemDTO>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await readRepo.Table
                .AsNoTracking()
                .OrderBy(i => i.Name)
                .ToListAsync(cancellationToken);

            return mapper.Map<List<ResultItemDTO>>(items);
        }
    }
}
