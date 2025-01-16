using Application.Interfaces.Services;
using Domain.Aggregates;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.Services
{
    public class ReportService : IReportService
    {
        public byte[] GenerateReport(IEnumerable<BookAuthorReportAggregate> data)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.Content()
                        .Column(col =>
                        {
                            col.Spacing(10);

                            // Título do relatório
                            col.Item().Text("Relatório de Livros por Autor")
                                .FontSize(20).Bold().Underline().AlignCenter();

                            var groupedData = data.GroupBy(d => d.AuthorName);

                            foreach (var group in groupedData)
                            {
                                // Nome do autor
                                col.Item().Column(authorColumn =>
                                {
                                    authorColumn.Item().Text($"Autor: {group.Key}")
                                        .FontSize(16).Bold().Underline();
                                    authorColumn.Spacing(5); // Espaçamento entre o título do autor e a tabela
                                });

                                // Tabela com os dados
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3); // Título
                                        columns.RelativeColumn(1); // Preço
                                        columns.RelativeColumn(2); // Assunto
                                    });

                                    // Cabeçalho da tabela
                                    table.Header(header =>
                                    {
                                        header.Cell().Text("Título").Bold();
                                        header.Cell().Text("Preço").Bold();
                                        header.Cell().Text("Assunto").Bold();
                                    });

                                    // Dados da tabela
                                    foreach (var item in group)
                                    {
                                        table.Cell().Text(item.BookTitle);
                                        table.Cell().Text($"{item.BookPrice:C}");
                                        table.Cell().Text(item.SubjectDescription);
                                    }
                                });
                            }
                        });
                });
            }).GeneratePdf();
        }
    }
}
