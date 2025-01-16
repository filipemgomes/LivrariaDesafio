using Domain.Common;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class BookValidator : BaseValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("O título não pode ser vazio.")
                .MaximumLength(255).WithMessage("O título deve ter no máximo 255 caracteres.");

            RuleFor(book => book.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(book => book.AuthorId)
                .GreaterThan(0).WithMessage("O autor deve ser válido.");

            RuleFor(book => book.SubjectId)
                .GreaterThan(0).WithMessage("O assunto deve ser válido.");
        }
    }
}
