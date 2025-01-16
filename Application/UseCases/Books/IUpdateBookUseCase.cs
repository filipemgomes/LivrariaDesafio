using Application.Models;

namespace Application.UseCases.Books
{
    public interface IUpdateBookUseCase
    {
        Task ExecuteAsync(int id, BookInputModel inputModel);
    }
}
