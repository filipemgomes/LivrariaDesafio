using Domain.Common;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AutorValidator : BaseValidator<Author>
    {
        public AutorValidator()
        {
            RuleFor(autor => autor.Name)
                .NotEmpty().WithMessage("O nome do autor não pode ser vazio.")
                .MaximumLength(255).WithMessage("O nome do autor deve ter no máximo 255 caracteres.");
        }
    }
}
