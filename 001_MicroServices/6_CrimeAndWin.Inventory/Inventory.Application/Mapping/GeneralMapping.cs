using AutoMapper;
using Inventory.Application.DTOs.InventoryDTOs;
using Inventory.Application.DTOs.ItemDTOs;
using Inventory.Application.DTOs.VODTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.VOs;

namespace Inventory.Application.Mapping
{
    public sealed class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Item, ResultItemDTO>().ReverseMap();
            CreateMap<ItemStats, ResultItemStatsDTO>().ReverseMap();
            CreateMap<ItemValue, ResultItemValueDTO>().ReverseMap();
            CreateMap<Domain.Entities.Inventory, ResultInventoryDTO>().ReverseMap();
        }
    }
}
