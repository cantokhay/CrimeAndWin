namespace Administration.MVC.Services.Dtos;

// Energy
public record PlayerEnergyDto
{
    public Guid     PlayerId        { get; init; }
    public string   PlayerName      { get; init; } = string.Empty;
    public int      CurrentEnergy   { get; init; }
    public int      MaxEnergy       { get; init; }
    public DateTime LastRefillAt    { get; init; }
    public string?  ActiveBoostItem { get; init; }
    public DateTime? BoostExpiresAt { get; init; }
}

// Cooldown
public record CooldownDto
{
    public Guid     PlayerId       { get; init; }
    public string   PlayerName     { get; init; } = string.Empty;
    public string   ActionType     { get; init; } = string.Empty;
    public DateTime CooldownEndsAt { get; init; }
    public int      RemainingSeconds { get; init; }
}

// ActionLog
public record ActionLogDto
{
    public Guid     ActionId      { get; init; }
    public Guid     PlayerId      { get; init; }
    public string   PlayerName    { get; init; } = string.Empty;
    public string   ActionType    { get; init; } = string.Empty;
    public bool     IsSuccess     { get; init; }
    public double   SuccessRate   { get; init; }
    public double   LevelBonus    { get; init; }
    public double   EquipBonus    { get; init; }
    public double   RandomVariation { get; init; }
    public int      EnergyCost    { get; init; }
    public DateTime ActionAt      { get; init; }
}

// Saga
public record SagaStateDto
{
    public Guid     CorrelationId { get; init; }
    public string   SagaType      { get; init; } = string.Empty;
    public Guid     PlayerId      { get; init; }
    public string   CurrentState  { get; init; } = string.Empty;
    public DateTime CreatedAt     { get; init; }
    public DateTime? CompletedAt  { get; init; }
    public string?  FailReason    { get; init; }
}

public record SagaDetailDto : SagaStateDto
{
    public bool EconomyDone   { get; init; }
    public bool InventoryDone { get; init; }
    public bool ProfileDone   { get; init; }
}

// Boost
public record ActiveBoostDto
{
    public Guid     PlayerId       { get; init; }
    public string   PlayerName     { get; init; } = string.Empty;
    public string   BoostItem      { get; init; } = string.Empty;
    public DateTime BoostExpiresAt { get; init; }
    public int      RemainingSeconds { get; init; }
}

// Health
public record ServiceHealthDto
{
    public string   ServiceName  { get; init; } = string.Empty;
    public bool     IsHealthy    { get; init; }
    public int      StatusCode   { get; init; }
    public string?  ErrorMessage { get; init; }
    public DateTime CheckedAt    { get; init; }
}

// GatewayLog
public record GatewayLogDto
{
    public DateTime Timestamp    { get; init; }
    public string   Method       { get; init; } = string.Empty;
    public string   Path         { get; init; } = string.Empty;
    public int      StatusCode   { get; init; }
    public double   ElapsedMs    { get; init; }
    public string?  ClientIp     { get; init; }
}
