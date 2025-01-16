using Application.Models;
using Application.UseCases.Books;
using Domain.Entities;
using Domain.Enums;
using Domain.Notifications;
using Domain.Repositories;
using Moq;
using Xunit;

namespace UnitTests.UseCases
{
    public class AddBookUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldAddBook_WhenValidDataProvided()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockSubjectRepository = new Mock<ISubjectRepository>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new AddBookUseCase(
                mockBookRepository.Object,
                mockAuthorRepository.Object,
                mockSubjectRepository.Object,
                notificationHandler);

            mockAuthorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                .ReturnsAsync(Mock.Of<Author>(a => a.Id == 1 && a.Name == "Sample Author"));
            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync(Mock.Of<Subject>(s => s.Id == 1 && s.Description == "Sample Subject"));

            var inputModel = new BookInputModel
            {
                Title = "Valid Book",
                BasePrice = 100,
                PurchaseMode = "Internet", // Simula compra pela internet
                AuthorId = 1,
                SubjectId = 1
            };

            // Act
            await useCase.ExecuteAsync(inputModel);

            // Assert
            mockBookRepository.Verify(repo => repo.AddAsync(It.Is<Book>(b =>
                b.Title == "Valid Book" &&
                b.Price == 85 && // 15% desconto aplicado
                b.AuthorId == 1 &&
                b.SubjectId == 1 &&
                b.PurchaseMode == PurchaseMode.Internet)), Times.Once);

            Assert.False(notificationHandler.HasNotifications());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenAuthorNotFound()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockSubjectRepository = new Mock<ISubjectRepository>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new AddBookUseCase(
                mockBookRepository.Object,
                mockAuthorRepository.Object,
                mockSubjectRepository.Object,
                notificationHandler);

            mockAuthorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                .ReturnsAsync((Author?)null); // Simula autor inexistente
            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync(Mock.Of<Subject>(s => s.Id == 1 && s.Description == "Sample Subject"));

            var inputModel = new BookInputModel
            {
                Title = "Valid Book",
                BasePrice = 100,
                PurchaseMode = "Internet", // Simula compra pela internet
                AuthorId = 1,
                SubjectId = 1
            };

            // Act
            await useCase.ExecuteAsync(inputModel);

            // Assert
            mockBookRepository.Verify(repo => repo.AddAsync(It.IsAny<Book>()), Times.Never);
            Assert.True(notificationHandler.HasNotifications());
            Assert.Contains(notificationHandler.GetNotifications(),
                n => n.Message == "O autor especificado não existe.");
        }
    }
}
