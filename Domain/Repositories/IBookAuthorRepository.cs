using Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBookAuthorRepository
    {
        Task<IEnumerable<BookAuthorReportAggregate>> GetBooksWithAuthorsAsync();
    }
}
