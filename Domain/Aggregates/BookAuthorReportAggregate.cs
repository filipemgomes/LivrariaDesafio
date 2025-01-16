namespace Domain.Aggregates
{
    public class BookAuthorReportAggregate
    {
        public string AuthorName { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }
        public string SubjectDescription { get; set; }
    }
}
