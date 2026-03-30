namespace Action.Domain.VOs
{
    public sealed record ActionRewards
        (
        int PowerGain, 
        bool ItemDrop, 
        decimal MoneyGain
        );
}
