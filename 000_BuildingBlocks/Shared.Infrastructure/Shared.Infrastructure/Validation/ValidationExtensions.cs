using FluentValidation;

namespace Shared.Infrastructure.Validation;

public static class ValidationExtensions
{
    // --- DATE VALIDATIONS ---

    public static IRuleBuilderOptions<T, DateTime> IsFuture<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Tarih gelecekte olmalidir.");
    }

    public static IRuleBuilderOptions<T, DateTime> IsPast<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date < DateTime.UtcNow)
            .WithMessage("Tarih gecmiste olmalidir.");
    }

    public static IRuleBuilderOptions<T, DateTime?> IsFuture<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
            .WithMessage("Tarih gelecekte olmalidir.");
    }

    public static IRuleBuilderOptions<T, DateTime> WithinDays<T>(this IRuleBuilder<T, DateTime> ruleBuilder, int days)
    {
        return ruleBuilder
            .Must(date => date <= DateTime.UtcNow.AddDays(days))
            .WithMessage($"Tarih en fazla {days} gun ileride olabilir.");
    }

    // --- NUMBER VALIDATIONS ---

    public static IRuleBuilderOptions<T, int> Positive<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder.GreaterThan(0).WithMessage("Deger 0'dan buyuk olmalidir.");
    }

    public static IRuleBuilderOptions<T, decimal> Positive<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.GreaterThan(0).WithMessage("Deger 0'dan buyuk olmalidir.");
    }

    public static IRuleBuilderOptions<T, decimal> PositiveCurrency<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .PrecisionScale(18, 2, false)
            .WithMessage("Miktar pozitif ve gecerli (18,2) formatinda olmalidir.");
    }

    public static IRuleBuilderOptions<T, int> InRange<T>(this IRuleBuilder<T, int> ruleBuilder, int min, int max)
    {
        return ruleBuilder
            .InclusiveBetween(min, max)
            .WithMessage($"Deger {min} ile {max} arasinda olmalidir.");
    }

    public static IRuleBuilderOptions<T, TProperty> IsInEnum<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder
            .IsInEnum()
            .WithMessage("Gecersiz secim.");
    }

    public static IRuleBuilderOptions<T, string> MatchesRegex<T>(this IRuleBuilder<T, string> ruleBuilder, string pattern, string description)
    {
        return ruleBuilder
            .Matches(pattern)
            .WithMessage($"{description} gecersiz formatta.");
    }
}
