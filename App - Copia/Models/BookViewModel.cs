namespace App.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal BasePrice { get; set; }
        public string PurchaseMode { get; set; }
        public int AuthorId { get; set; }
        public int SubjectId { get; set; }
    }
}
