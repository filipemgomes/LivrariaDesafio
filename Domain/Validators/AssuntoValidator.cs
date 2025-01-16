using Domain.Common;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AssuntoValidator : BaseValidator<Subject>
    {
        public AssuntoValidator()
        {
            RuleFor(assunto => assunto.Description)
                .NotEmpty().WithMessage("A descrição do assunto não pode ser vazia.")
                .MaximumLength(255).WithMessage("A descrição do assunto deve ter no máximo 255 caracteres.");
        }
    }
}
