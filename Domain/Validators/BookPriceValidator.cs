using Domain.Common;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class BookPriceValidator : BaseValidator<BookPrice>
    {
        public BookPriceValidator()
        {
            RuleFor(bookPrice => bookPrice.Id)
                .GreaterThan(0).WithMessage("O ID do livro deve ser maior que zero.");

            RuleFor(bookPrice => bookPrice.PurchaseMode)
                .IsInEnum().WithMessage("O modo de compra é inválido.");

            RuleFor(bookPrice => bookPrice.BasePrice)
                .GreaterThan(0).WithMessage("O preço base deve ser maior que zero.");

            RuleFor(bookPrice => bookPrice.DiscountPercentage)
                .InclusiveBetween(0, 100).WithMessage("A porcentagem de desconto deve estar entre 0 e 100.");
        }
    }
}
