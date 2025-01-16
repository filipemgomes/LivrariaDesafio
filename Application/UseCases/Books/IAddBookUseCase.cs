using Application.Models;

namespace Application.UseCases.Books
{
    public interface IAddBookUseCase
    {
        Task ExecuteAsync(BookInputModel inputModel);
    }
}
