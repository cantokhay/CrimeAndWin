using Action.Domain.Enums;

namespace Action.Domain.VOs
{
    public sealed record PlayerActionResult
        (
        double SuccessRate, 
        OutcomeType OutcomeType
        );
}
