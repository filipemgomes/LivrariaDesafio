using Application.UseCases.Reports.GetBooksWithAuthors;
using Domain.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1.Reports
{
    [ApiController]
    [Route("api/v1/reports/[controller]")]
    public class BookAuthorReportController : ControllerBase
    {
        private readonly IGetBooksWithAuthorsUseCase _useCase;
        private readonly DomainNotificationHandler _notificationHandler;

        public BookAuthorReportController(
            IGetBooksWithAuthorsUseCase useCase,
            DomainNotificationHandler notificationHandler)
        {
            _useCase = useCase;
            _notificationHandler = notificationHandler;
        }

        /// <summary>
        /// Gera um relatório em PDF com dados de livros agrupados por autor.
        /// </summary>
        /// <returns>Arquivo PDF gerado</returns>
        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult), 200)] // PDF gerado com sucesso
        [ProducesResponseType(400)] // Problemas de validação ou erro do relatório
        [ProducesResponseType(404)] // Nenhum dado encontrado
        public async Task<IActionResult> GetReport()
        {
            var pdfData = await _useCase.ExecuteAsync();
                        
            if (_notificationHandler.HasNotifications())
            {
                var errors = _notificationHandler.GetNotifications()
                    .Select(n => n.Message);
                                
                return BadRequest(new { Errors = errors });
            }
                        
            if (pdfData == null || pdfData.Length == 0)
                return NotFound(new { Message = "Nenhum dado encontrado para gerar o relatório." });
                        
            return File(pdfData, "application/pdf", "RelatorioLivrosAutores.pdf");
        }
    }
}
