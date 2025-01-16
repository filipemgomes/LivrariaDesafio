using Domain.Aggregates;

namespace Application.Interfaces.Services
{
    public interface IReportService
    {
        /// <summary>
        /// Gera um relatório em PDF a partir de uma lista de agregados.
        /// </summary>
        /// <param name="data">Dados agregados para o relatório.</param>
        /// <returns>Relatório em PDF como array de bytes.</returns>
        byte[] GenerateReport(IEnumerable<BookAuthorReportAggregate> data);
    }
}
