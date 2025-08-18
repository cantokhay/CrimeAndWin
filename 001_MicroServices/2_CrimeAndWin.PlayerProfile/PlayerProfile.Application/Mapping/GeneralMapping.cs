using AutoMapper;
using PlayerProfile.Application.Features.Player.DTOs;

namespace PlayerProfile.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Player, CreatePlayerDTO>()
                .ForMember(d => d.Power, m => m.MapFrom(s => s.Stats.Power))
                .ForMember(d => d.Defense, m => m.MapFrom(s => s.Stats.Defense))
                .ForMember(d => d.Agility, m => m.MapFrom(s => s.Stats.Agility))
                .ForMember(d => d.Luck, m => m.MapFrom(s => s.Stats.Luck))
                .ForMember(d => d.EnergyCurrent, m => m.MapFrom(s => s.Energy.Current))
                .ForMember(d => d.EnergyMax, m => m.MapFrom(s => s.Energy.Max))
                .ForMember(d => d.EnergyRegenPerMinute, m => m.MapFrom(s => s.Energy.RegenPerMinute))
                .ForMember(d => d.RankPoints, m => m.MapFrom(s => s.Rank.RankPoints))
                .ForMember(d => d.RankPosition, m => m.MapFrom(s => s.Rank.Position))
                .ReverseMap();

            CreateMap<Domain.Entities.Player, UpdatePlayerDTO>()
                .ForMember(d => d.Power, m => m.MapFrom(s => s.Stats.Power))
                .ForMember(d => d.Defense, m => m.MapFrom(s => s.Stats.Defense))
                .ForMember(d => d.Agility, m => m.MapFrom(s => s.Stats.Agility))
                .ForMember(d => d.Luck, m => m.MapFrom(s => s.Stats.Luck))
                .ForMember(d => d.EnergyCurrent, m => m.MapFrom(s => s.Energy.Current))
                .ForMember(d => d.EnergyMax, m => m.MapFrom(s => s.Energy.Max))
                .ForMember(d => d.EnergyRegenPerMinute, m => m.MapFrom(s => s.Energy.RegenPerMinute))
                .ForMember(d => d.RankPoints, m => m.MapFrom(s => s.Rank.RankPoints))
                .ForMember(d => d.RankPosition, m => m.MapFrom(s => s.Rank.Position))
                .ForMember(d => d.CreatedAtUtc, m => m.MapFrom(s => s.CreatedAtUtc))
                .ReverseMap();

            CreateMap<Domain.Entities.Player, ResultPlayerDTO>()
                .ForMember(d => d.Power, m => m.MapFrom(s => s.Stats.Power))
                .ForMember(d => d.Defense, m => m.MapFrom(s => s.Stats.Defense))
                .ForMember(d => d.Agility, m => m.MapFrom(s => s.Stats.Agility))
                .ForMember(d => d.Luck, m => m.MapFrom(s => s.Stats.Luck))
                .ForMember(d => d.EnergyCurrent, m => m.MapFrom(s => s.Energy.Current))
                .ForMember(d => d.EnergyMax, m => m.MapFrom(s => s.Energy.Max))
                .ForMember(d => d.EnergyRegenPerMinute, m => m.MapFrom(s => s.Energy.RegenPerMinute))
                .ForMember(d => d.RankPoints, m => m.MapFrom(s => s.Rank.RankPoints))
                .ForMember(d => d.RankPosition, m => m.MapFrom(s => s.Rank.Position))
                .ReverseMap();
        }
    }
}
