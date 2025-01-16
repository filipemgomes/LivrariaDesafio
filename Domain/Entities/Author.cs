using Domain.Common;
using Domain.Validators;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; private set; }
        public ICollection<Book> Books { get; private set; }

        protected Author()
        {
            Books = new List<Book>();
        }

        public Author(string name)
        {
            Name = name;
            Books = new List<Book>();

            Validate();
        }

        public override bool Validate()
        {
            var validator = new AutorValidator();
            ValidationResult = validator.Validate(this);

            foreach (var error in ValidationResult.Errors)
                AddNotification(error.ErrorMessage);

            return ValidationResult.IsValid;
        }
    }
}
