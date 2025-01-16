namespace Application.Models
{
    public class BookInputModel
    {
        public string Title { get; set; }
        public decimal BasePrice { get; set; } // Preço base necessário
        public string PurchaseMode { get; set; } // "Balcao", "SelfService", etc.
        public int AuthorId { get; set; }
        public int SubjectId { get; set; }
    }
}
