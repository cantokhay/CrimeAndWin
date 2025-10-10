using Action.Application.DTOs;
using Action.Domain.Entities;
using Action.Domain.Enums;
using Action.Domain.VOs;
using AutoMapper;

namespace Action.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<ActionDefinition, ActionDefinitionDTO>().ReverseMap();

            CreateMap<CreateActionDefinitionDTO, ActionDefinition>()
                .ForMember(d => d.Requirements,
                    o => o.MapFrom(s => new ActionRequirements(s.MinPower, s.EnergyCost)))
                .ForMember(d => d.Rewards,
                    o => o.MapFrom(s => new ActionRewards(s.PowerGain, s.ItemDrop, s.MoneyGain)))
                .ReverseMap();

            CreateMap<PlayerActionAttemptDTO, PlayerActionAttempt>()
                .ForMember(d => d.PlayerActionResults,
                    o => o.MapFrom(s => new PlayerActionResult(s.SuccessRate,
                        s.SuccessRate >= 0.5 ? OutcomeType.Success : OutcomeType.Fail)))
                .ReverseMap();

            CreateMap<ActionDefinition, ResultActionDefinitionDTO>()
                .ForMember(dest => dest.MinPower, opt => opt.MapFrom(src => src.Requirements.MinPower))
                .ForMember(dest => dest.EnergyCost, opt => opt.MapFrom(src => src.Requirements.EnergyCost))
                .ForMember(dest => dest.PowerGain, opt => opt.MapFrom(src => src.Rewards.PowerGain))
                .ForMember(dest => dest.ItemDrop, opt => opt.MapFrom(src => src.Rewards.ItemDrop))
                .ForMember(dest => dest.MoneyGain, opt => opt.MapFrom(src => src.Rewards.MoneyGain))
                .ReverseMap();

            CreateMap<PlayerActionAttempt, ResultPlayerActionAttemptDTO>()
                .ForMember(dest => dest.SuccessRate, opt => opt.MapFrom(src => src.PlayerActionResults.SuccessRate))
                .ForMember(dest => dest.OutcomeType, opt => opt.MapFrom(src => src.PlayerActionResults.OutcomeType))
                .ReverseMap();
        }
    }
}
