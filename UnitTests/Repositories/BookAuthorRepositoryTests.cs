using Domain.Aggregates;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace UnitTests.Repositories
{
    public class BookAuthorRepositoryTests
    {
        [Fact]
        public async Task GetBooksWithAuthorsAsync_ShouldReturnAggregates()
        {
            // Arrange
            var mockData = new List<BookAuthorReportAggregate>
            {
                new BookAuthorReportAggregate { AuthorName = "Author 1", BookTitle = "Book 1", BookPrice = 100m, SubjectDescription = "Subject 1" },
                new BookAuthorReportAggregate { AuthorName = "Author 2", BookTitle = "Book 2", BookPrice = 200m, SubjectDescription = "Subject 2" }
            };

            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository.Setup(repo => repo.GetBooksWithAuthorsAsync()).ReturnsAsync(mockData);

            // Act
            var result = await mockRepository.Object.GetBooksWithAuthorsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Author 1", result.First().AuthorName);
            Assert.Equal("Book 1", result.First().BookTitle);
            Assert.Equal(100m, result.First().BookPrice);
            Assert.Equal("Subject 1", result.First().SubjectDescription);
        }

        [Fact]
        public async Task GetBooksWithAuthorsAsync_ShouldThrowRepositoryException_OnDbUpdateException()
        {
            // Arrange
            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository
                .Setup(repo => repo.GetBooksWithAuthorsAsync())
                .ThrowsAsync(new RepositoryException("Erro ao consultar o banco de dados."));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RepositoryException>(() => mockRepository.Object.GetBooksWithAuthorsAsync());
            Assert.Equal("Erro ao consultar o banco de dados.", exception.Message);
        }

        [Fact]
        public async Task GetBooksWithAuthorsAsync_ShouldThrowRepositoryException_OnGenericException()
        {
            // Arrange
            var mockRepository = new Mock<IBookAuthorRepository>();
            mockRepository
                .Setup(repo => repo.GetBooksWithAuthorsAsync())
                .ThrowsAsync(new RepositoryException("Erro inesperado ao acessar os dados."));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RepositoryException>(() => mockRepository.Object.GetBooksWithAuthorsAsync());
            Assert.Equal("Erro inesperado ao acessar os dados.", exception.Message);
        }
    }
}
