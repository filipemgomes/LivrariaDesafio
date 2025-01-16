namespace Application.UseCases.Reports.GetBooksWithAuthors
{
    public interface IGetBooksWithAuthorsUseCase
    {
        Task<byte[]> ExecuteAsync();
    }
}
