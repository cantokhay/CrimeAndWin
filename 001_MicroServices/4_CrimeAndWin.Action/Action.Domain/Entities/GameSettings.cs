using System;
using Shared.Domain;

namespace Action.Domain.Entities;

public class GameSettings : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}


