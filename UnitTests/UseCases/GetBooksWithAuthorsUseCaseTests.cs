using Application.Interfaces.Services;
using Application.UseCases.Reports.GetBooksWithAuthors;
using Domain.Aggregates;
using Domain.Exceptions;
using Domain.Notifications;
using Domain.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.UseCases
{
    public class GetBooksWithAuthorsUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnPdf_WhenDataExists()
        {
            // Arrange: Dados simulados
            var mockData = new List<BookAuthorReportAggregate>
            {
                new BookAuthorReportAggregate { AuthorName = "Author 1", BookTitle = "Book 1", BookPrice = 100m, SubjectDescription = "Subject 1" }
            };
            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository.Setup(repo => repo.GetBooksWithAuthorsAsync()).ReturnsAsync(mockData);

            var mockReportService = new Mock<IReportService>();
            mockReportService.Setup(service => service.GenerateReport(It.IsAny<IEnumerable<BookAuthorReportAggregate>>()))
                             .Returns(new byte[] { 1, 2, 3 });

            var notificationHandler = new DomainNotificationHandler();

            var useCase = new GetBooksWithAuthorsUseCase(mockRepository.Object, mockReportService.Object, notificationHandler);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new byte[] { 1, 2, 3 }, result);
            Assert.False(notificationHandler.HasNotifications());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenNoDataExists()
        {
            // Arrange
            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository.Setup(repo => repo.GetBooksWithAuthorsAsync()).ReturnsAsync(new List<BookAuthorReportAggregate>());

            var mockReportService = new Mock<IReportService>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new GetBooksWithAuthorsUseCase(mockRepository.Object, mockReportService.Object, notificationHandler);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal(Array.Empty<byte>(), result); // Verifica se retorna Array.Empty<byte>()
            Assert.True(notificationHandler.HasNotifications());
            Assert.Equal("Nenhum dado encontrado para gerar o relatório.", notificationHandler.GetNotifications().First().Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenRepositoryThrowsException()
        {
            // Arrange
            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository.Setup(repo => repo.GetBooksWithAuthorsAsync()).ThrowsAsync(new RepositoryException("Erro simulado no repositório."));

            var mockReportService = new Mock<IReportService>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new GetBooksWithAuthorsUseCase(mockRepository.Object, mockReportService.Object, notificationHandler);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal(Array.Empty<byte>(), result); // Verifica se retorna Array.Empty<byte>()
            Assert.True(notificationHandler.HasNotifications());
            Assert.Equal("Erro ao acessar os dados: Erro simulado no repositório.", notificationHandler.GetNotifications().First().Message);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenReportServiceFails()
        {
            // Arrange
            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository.Setup(repo => repo.GetBooksWithAuthorsAsync())
                          .ReturnsAsync(new List<BookAuthorReportAggregate>
                          {
                              new BookAuthorReportAggregate { AuthorName = "Author 1", BookTitle = "Book 1", BookPrice = 100m, SubjectDescription = "Subject 1" }
                          });

            var mockReportService = new Mock<IReportService>();
            mockReportService.Setup(service => service.GenerateReport(It.IsAny<IEnumerable<BookAuthorReportAggregate>>()))
                             .Throws(new System.Exception("Erro ao gerar o relatório."));

            var notificationHandler = new DomainNotificationHandler();

            var useCase = new GetBooksWithAuthorsUseCase(mockRepository.Object, mockReportService.Object, notificationHandler);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            Assert.Equal(Array.Empty<byte>(), result); // Verifica se retorna Array.Empty<byte>()
            Assert.True(notificationHandler.HasNotifications());
            Assert.Equal("Erro inesperado: Erro ao gerar o relatório.", notificationHandler.GetNotifications().First().Message);
        }
    }
}
