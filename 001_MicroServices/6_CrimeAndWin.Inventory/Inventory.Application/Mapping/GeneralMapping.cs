using AutoMapper;
using Inventory.Application.DTOs.InventoryDTOs;
using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Inventory.Application.DTOs.ItemDTOs;
using Inventory.Application.DTOs.ItemDTOs.Admin;
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

            // ==========================
            //        ADMIN MAPPINGS
            // ==========================

            // Inventory Admin
            CreateMap<Domain.Entities.Inventory, AdminResultInventoryDTO>()
                .ForMember(d => d.Items, o => o.Ignore()) // Query tarafında el ile dolduruyoruz
                .ReverseMap();

            CreateMap<AdminCreateInventoryDTO, Domain.Entities.Inventory>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Items, o => o.Ignore())
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore());

            CreateMap<AdminUpdateInventoryDTO, Domain.Entities.Inventory>()
                .ForMember(d => d.Items, o => o.Ignore())
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore());

            // Item Admin
            CreateMap<Domain.Entities.Item, AdminResultItemDTO>()
                .ForMember(d => d.Damage, o => o.MapFrom(s => s.Stats.Damage))
                .ForMember(d => d.Defense, o => o.MapFrom(s => s.Stats.Defense))
                .ForMember(d => d.Power, o => o.MapFrom(s => s.Stats.Power))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Value.Amount))
                .ForMember(d => d.Currency, o => o.MapFrom(s => s.Value.Currency))
                .ReverseMap();

            CreateMap<AdminCreateItemDTO, Domain.Entities.Item>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Stats, o => o.MapFrom(s => new ItemStats(s.Damage, s.Defense, s.Power)))
                .ForMember(d => d.Value, o => o.MapFrom(s => new ItemValue(s.Amount, s.Currency)))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.Inventory, o => o.Ignore());

            CreateMap<AdminUpdateItemDTO, Domain.Entities.Item>()
                .ForMember(d => d.Stats, o => o.MapFrom(s => new ItemStats(s.Damage, s.Defense, s.Power)))
                .ForMember(d => d.Value, o => o.MapFrom(s => new ItemValue(s.Amount, s.Currency)))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.Inventory, o => o.Ignore());
        }
    }
}
