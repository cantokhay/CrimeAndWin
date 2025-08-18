namespace Shared.Domain.Time
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
