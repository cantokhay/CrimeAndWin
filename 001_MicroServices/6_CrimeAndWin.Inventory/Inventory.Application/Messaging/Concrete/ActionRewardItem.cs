namespace Inventory.Application.Messaging.Concrete
{
    public sealed record ActionRewardItem
        (
        string Name, 
        int Quantity, 
        int Damage, 
        int Defense, 
        int Power, 
        decimal Amount, 
        int Currency
        );
}
