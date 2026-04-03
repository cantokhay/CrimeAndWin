using Inventory.Application.DTOs.ItemDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Item.Commands.GetAllItems
{
    public sealed record GetAllItemsQuery() : IRequest<List<ResultItemDTO>>;
}


