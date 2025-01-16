using Domain.Common;
using Domain.Validators;

namespace Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Description { get; private set; }
        public ICollection<Book> Books { get; private set; }

        protected Subject()
        {
            Books = new List<Book>();
        }

        public Subject(string description)
        {
            Description = description;
            Books = new List<Book>();

            Validate();
        }

        public override bool Validate()
        {
            var validator = new AssuntoValidator();
            ValidationResult = validator.Validate(this);

            foreach (var error in ValidationResult.Errors)
                AddNotification(error.ErrorMessage);

            return ValidationResult.IsValid;
        }
    }
}
