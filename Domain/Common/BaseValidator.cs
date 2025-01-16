using FluentValidation;

namespace Domain.Common
{
    public abstract class BaseValidator<T> : AbstractValidator<T> where T : BaseEntity
    {
        protected BaseValidator()
        {
            RuleFor(entity => entity.Id)
                .GreaterThan(0).WithMessage("O ID deve ser maior que zero.");
        }
    }
}
