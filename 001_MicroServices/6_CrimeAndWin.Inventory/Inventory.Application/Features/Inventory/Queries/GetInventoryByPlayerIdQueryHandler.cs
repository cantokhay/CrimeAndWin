using AutoMapper;
using Inventory.Application.DTOs.InventoryDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Inventory.Application.Features.Inventory.Queries
{
    public sealed class GetInventoryByPlayerIdQueryHandler :
        IRequestHandler<GetInventoryByPlayerIdQuery, ResultInventoryDTO>
    {
        private readonly IReadRepository<Domain.Entities.Inventory> _read;
        private readonly IMapper _mapper;

        public GetInventoryByPlayerIdQueryHandler(IReadRepository<Domain.Entities.Inventory> read, IMapper mapper)
        {
            _read = read;
            _mapper = mapper;
        }

        public async Task<ResultInventoryDTO> Handle(GetInventoryByPlayerIdQuery request, CancellationToken ct)
        {
            var inv = await _read.GetWhere(x => x.PlayerId == request.PlayerId, tracking: false)
                                 .Include(x => x.Items)
                                 .FirstOrDefaultAsync(ct);

            return inv is null ? null : _mapper.Map<ResultInventoryDTO>(inv);
        }
    }
}
