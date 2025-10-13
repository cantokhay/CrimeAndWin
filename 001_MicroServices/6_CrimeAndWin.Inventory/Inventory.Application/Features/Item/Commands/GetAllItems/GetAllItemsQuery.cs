using Inventory.Application.DTOs.ItemDTOs;
using MediatR;

namespace Inventory.Application.Features.Item.Commands.GetAllItems
{
    public sealed record GetAllItemsQuery() : IRequest<List<ResultItemDTO>>;
}
