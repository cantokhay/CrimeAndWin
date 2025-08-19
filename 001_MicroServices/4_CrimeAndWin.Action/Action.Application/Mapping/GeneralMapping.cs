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
        }
    }
}
