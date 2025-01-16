using Domain.Common;
using Domain.Enums;
using FluentValidation;

namespace Domain.Entities
{
    public class BookPrice : BaseEntity
    {
        public Book Book { get; private set; }
        public PurchaseMode PurchaseMode { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal DiscountPercentage { get; private set; }

        protected BookPrice() { }

        public BookPrice(int bookId, PurchaseMode purchaseMode, decimal basePrice, decimal discountPercentage)
        {
            Id = bookId;
            PurchaseMode = purchaseMode;
            BasePrice = basePrice;
            DiscountPercentage = discountPercentage;
        }

        public decimal CalculateFinalPrice()
        {
            return BasePrice * (1 - DiscountPercentage / 100);
        }

        public override bool Validate()
        {
            var validator = new Validators.BookPriceValidator();
            ValidationResult = validator.Validate(this);

            foreach (var error in ValidationResult.Errors)
                AddNotification(error.ErrorMessage);

            return ValidationResult.IsValid;
        }
    }
}
