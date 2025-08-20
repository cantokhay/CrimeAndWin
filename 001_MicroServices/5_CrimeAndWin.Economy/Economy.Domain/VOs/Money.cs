namespace Economy.Domain.VOs
{
    public record Money
        (
        decimal Amount, 
        string CurrencyType
        );
}
