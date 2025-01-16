using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public int AuthorId { get; private set; }
        public Author Author { get; private set; }
        public int SubjectId { get; private set; }
        public Subject Subject { get; private set; }
        public PurchaseMode PurchaseMode { get; private set; } // Adicionado campo PurchaseMode

        protected Book() { }

        public Book(string title, int authorId, int subjectId, PurchaseMode purchaseMode, decimal basePrice)
        {
            Title = title;
            AuthorId = authorId;
            SubjectId = subjectId;
            PurchaseMode = purchaseMode;

            SetPrice(basePrice, purchaseMode);
            Validate();
        }

        public void UpdateDetails(string title, decimal basePrice, int authorId, int subjectId, PurchaseMode purchaseMode)
        {
            Title = title;
            AuthorId = authorId;
            SubjectId = subjectId;
            PurchaseMode = purchaseMode;

            SetPrice(basePrice, purchaseMode);
            Validate();
        }

        public void SetPrice(decimal basePrice, PurchaseMode purchaseMode)
        {
            Price = purchaseMode switch
            {
                PurchaseMode.Balcao => basePrice,
                PurchaseMode.SelfService => basePrice * 0.9m, // 10% de desconto
                PurchaseMode.Internet => basePrice * 0.85m,  // 15% de desconto
                PurchaseMode.Evento => basePrice * 0.8m,     // 20% de desconto
                _ => basePrice
            };
        }

        public override bool Validate()
        {
            var validator = new Validators.BookValidator();
            ValidationResult = validator.Validate(this);

            foreach (var error in ValidationResult.Errors)
                AddNotification(error.ErrorMessage);

            return ValidationResult.IsValid;
        }
    }
}
