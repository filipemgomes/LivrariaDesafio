using Domain.Aggregates;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly AppDbContext _context;

        public BookAuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookAuthorReportAggregate>> GetBooksWithAuthorsAsync()
        {
            try
            {                
                var data = await _context.BookAuthorViews.AsNoTracking().ToListAsync();
                                
                return data.Select(view => new BookAuthorReportAggregate
                {
                    AuthorName = view.AuthorName,
                    BookTitle = view.BookTitle,
                    BookPrice = view.BookPrice,
                    SubjectDescription = view.SubjectDescription
                });
            }
            catch (DbUpdateException ex)
            {                
                throw new RepositoryException("Erro ao consultar a view no banco de dados.", ex);
            }
            catch (Exception ex)
            {                
                throw new RepositoryException("Erro inesperado ao acessar os dados.", ex);
            }
        }
    }
}
