using Application.Interfaces.Services;
using Domain.Exceptions;
using Domain.Notifications;
using Domain.Repositories;

namespace Application.UseCases.Reports.GetBooksWithAuthors
{
    public class GetBooksWithAuthorsUseCase : IGetBooksWithAuthorsUseCase
    {
        private readonly IBookAuthorRepository _repository;
        private readonly IReportService _reportService;
        private readonly DomainNotificationHandler _notificationHandler;

        public GetBooksWithAuthorsUseCase(
            IBookAuthorRepository repository,
            IReportService reportService,
            DomainNotificationHandler notificationHandler)
        {
            _repository = repository;
            _reportService = reportService;
            _notificationHandler = notificationHandler;
        }

        public async Task<byte[]> ExecuteAsync()
        {
            try
            {                
                var booksWithAuthors = await _repository.GetBooksWithAuthorsAsync();

                if (booksWithAuthors == null || !booksWithAuthors.Any())
                {
                    _notificationHandler.AddNotification(new DomainNotification("Relatório", "Nenhum dado encontrado para gerar o relatório."));
                    return Array.Empty<byte>();
                }
                                
                var pdf = _reportService.GenerateReport(booksWithAuthors);
                return pdf ?? Array.Empty<byte>();
            }
            catch (RepositoryException ex)
            {                
                _notificationHandler.AddNotification(new DomainNotification("Banco de Dados", $"Erro ao acessar os dados: {ex.Message}"));
                return Array.Empty<byte>();
            }
            catch (Exception ex)
            {                
                _notificationHandler.AddNotification(new DomainNotification("Relatório", $"Erro inesperado: {ex.Message}"));
                return Array.Empty<byte>();
            }
        }
    }
}
