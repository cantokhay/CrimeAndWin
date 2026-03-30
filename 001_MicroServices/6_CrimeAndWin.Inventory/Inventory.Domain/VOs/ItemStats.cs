namespace Inventory.Domain.VOs
{
    public readonly record struct ItemStats
        (
        int Damage, 
        int Defense, 
        int Power
        );
}
