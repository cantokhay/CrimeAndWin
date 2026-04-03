using Action.Application.DTOs.ActionAttemptDTOs;
using Action.Application.DTOs.ActionAttemptDTOs.Admin;
using Action.Application.DTOs.ActionDefinitionDTOs;
using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Action.Domain.Entities;
using Action.Domain.Enums;
using Action.Domain.VOs;
using Riok.Mapperly.Abstractions;

namespace Action.Application.Mapping;

[Mapper]
public partial class ActionMapper
{
    // ActionDefinition
    public partial ActionDefinitionDTO ToDto(ActionDefinition entity);
    public partial ActionDefinition ToEntity(ActionDefinitionDTO dto);

    [MapProperty(nameof(CreateActionDefinitionDTO.MinPower), "Requirements.MinPower")]
    [MapProperty(nameof(CreateActionDefinitionDTO.EnergyCost), "Requirements.EnergyCost")]
    [MapProperty(nameof(CreateActionDefinitionDTO.PowerGain), "Rewards.PowerGain")]
    [MapProperty(nameof(CreateActionDefinitionDTO.ItemDrop), "Rewards.ItemDrop")]
    [MapProperty(nameof(CreateActionDefinitionDTO.MoneyGain), "Rewards.MoneyGain")]
    public partial ActionDefinition ToEntity(CreateActionDefinitionDTO dto);
    [MapProperty("Requirements.MinPower", nameof(CreateActionDefinitionDTO.MinPower))]
    [MapProperty("Requirements.EnergyCost", nameof(CreateActionDefinitionDTO.EnergyCost))]
    [MapProperty("Rewards.PowerGain", nameof(CreateActionDefinitionDTO.PowerGain))]
    [MapProperty("Rewards.ItemDrop", nameof(CreateActionDefinitionDTO.ItemDrop))]
    [MapProperty("Rewards.MoneyGain", nameof(CreateActionDefinitionDTO.MoneyGain))]
    public partial CreateActionDefinitionDTO ToCreateDto(ActionDefinition entity);

    [MapProperty(nameof(PlayerActionAttemptDTO.SuccessRate), "PlayerActionResults.SuccessRate")]
    public partial PlayerActionAttempt ToEntity(PlayerActionAttemptDTO dto);

    [MapProperty(nameof(ActionDefinition.Requirements.MinPower), nameof(ResultActionDefinitionDTO.MinPower))]
    [MapProperty(nameof(ActionDefinition.Requirements.EnergyCost), nameof(ResultActionDefinitionDTO.EnergyCost))]
    [MapProperty(nameof(ActionDefinition.Rewards.PowerGain), nameof(ResultActionDefinitionDTO.PowerGain))]
    [MapProperty(nameof(ActionDefinition.Rewards.ItemDrop), nameof(ResultActionDefinitionDTO.ItemDrop))]
    [MapProperty(nameof(ActionDefinition.Rewards.MoneyGain), nameof(ResultActionDefinitionDTO.MoneyGain))]
    public partial ResultActionDefinitionDTO ToResultDto(ActionDefinition entity);

    [MapProperty(nameof(PlayerActionAttempt.PlayerActionResults.SuccessRate), nameof(ResultPlayerActionAttemptDTO.SuccessRate))]
    [MapProperty(nameof(PlayerActionAttempt.PlayerActionResults.OutcomeType), nameof(ResultPlayerActionAttemptDTO.OutcomeType))]
    public partial ResultPlayerActionAttemptDTO ToResultDto(PlayerActionAttempt entity);

    // Admin Mappings
    [MapProperty(nameof(AdminCreateActionDefinitionDTO.MinPower), "Requirements.MinPower")]
    [MapProperty(nameof(AdminCreateActionDefinitionDTO.EnergyCost), "Requirements.EnergyCost")]
    [MapProperty(nameof(AdminCreateActionDefinitionDTO.PowerGain), "Rewards.PowerGain")]
    [MapProperty(nameof(AdminCreateActionDefinitionDTO.ItemDrop), "Rewards.ItemDrop")]
    [MapProperty(nameof(AdminCreateActionDefinitionDTO.MoneyGain), "Rewards.MoneyGain")]
    public partial ActionDefinition ToEntity(AdminCreateActionDefinitionDTO dto);

    [MapProperty(nameof(AdminUpdateActionDefinitionDTO.MinPower), "Requirements.MinPower")]
    [MapProperty(nameof(AdminUpdateActionDefinitionDTO.EnergyCost), "Requirements.EnergyCost")]
    [MapProperty(nameof(AdminUpdateActionDefinitionDTO.PowerGain), "Rewards.PowerGain")]
    [MapProperty(nameof(AdminUpdateActionDefinitionDTO.ItemDrop), "Rewards.ItemDrop")]
    [MapProperty(nameof(AdminUpdateActionDefinitionDTO.MoneyGain), "Rewards.MoneyGain")]
    public partial ActionDefinition ToEntity(AdminUpdateActionDefinitionDTO dto);

    [MapProperty(nameof(ActionDefinition.Requirements.MinPower), nameof(AdminResultActionDefinitionDTO.MinPower))]
    [MapProperty(nameof(ActionDefinition.Requirements.EnergyCost), nameof(AdminResultActionDefinitionDTO.EnergyCost))]
    [MapProperty(nameof(ActionDefinition.Rewards.PowerGain), nameof(AdminResultActionDefinitionDTO.PowerGain))]
    [MapProperty(nameof(ActionDefinition.Rewards.ItemDrop), nameof(AdminResultActionDefinitionDTO.ItemDrop))]
    [MapProperty(nameof(ActionDefinition.Rewards.MoneyGain), nameof(AdminResultActionDefinitionDTO.MoneyGain))]
    public partial AdminResultActionDefinitionDTO ToAdminResultDto(ActionDefinition entity);

    [MapProperty(nameof(AdminCreatePlayerActionAttemptDTO.SuccessRate), "PlayerActionResults.SuccessRate")]
    public partial PlayerActionAttempt ToEntity(AdminCreatePlayerActionAttemptDTO dto);

    [MapProperty(nameof(AdminUpdatePlayerActionAttemptDTO.SuccessRate), "PlayerActionResults.SuccessRate")]
    public partial PlayerActionAttempt ToEntity(AdminUpdatePlayerActionAttemptDTO dto);

    [MapProperty(nameof(PlayerActionAttempt.PlayerActionResults.SuccessRate), nameof(AdminResultPlayerActionAttemptDTO.SuccessRate))]
    [MapProperty(nameof(PlayerActionAttempt.PlayerActionResults.OutcomeType), nameof(AdminResultPlayerActionAttemptDTO.OutcomeType))]
    public partial AdminResultPlayerActionAttemptDTO ToAdminResultDto(PlayerActionAttempt entity);

    // List Mappings
    public partial IEnumerable<ResultActionDefinitionDTO> ToResultDtoList(IEnumerable<ActionDefinition> entities);
    public partial IEnumerable<AdminResultActionDefinitionDTO> ToAdminResultDtoList(IEnumerable<ActionDefinition> entities);
    public partial IEnumerable<ResultPlayerActionAttemptDTO> ToResultDtoList(IEnumerable<PlayerActionAttempt> entities);
}


