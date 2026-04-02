using Inventory.Application.DTOs.ItemDTOs;
using Mediator;

namespace Inventory.Application.Features.Item.Commands.GetAllItems
{
    public sealed record GetAllItemsQuery() : IRequest<List<ResultItemDTO>>;
}

