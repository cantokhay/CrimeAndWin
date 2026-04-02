using FluentValidation;
using Shared.Domain;

namespace Shared.Infrastructure.Validation;

public abstract class BaseEntityValidator<T> : AbstractValidator<T> where T : BaseEntity
{
    protected BaseEntityValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID alanı boş olamaz.");
        
        RuleFor(x => x.CreatedAtUtc)
            .NotEmpty().WithMessage("Oluşturulma tarihi boş olamaz.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Oluşturulma tarihi gelecek bir zaman olamaz.");

        RuleFor(x => x.UpdatedAtUtc)
            .Must((entity, updatedAt) => !updatedAt.HasValue || updatedAt >= entity.CreatedAtUtc)
            .WithMessage("Güncelleme tarihi oluşturulma tarihinden önce olamaz.");
    }
}
