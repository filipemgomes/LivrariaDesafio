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
    public class UpdateBookUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenBookNotFound()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockSubjectRepository = new Mock<ISubjectRepository>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new UpdateBookUseCase(
                mockBookRepository.Object,
                mockAuthorRepository.Object,
                mockSubjectRepository.Object,
                notificationHandler);

            mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync((Book?)null);

            var inputModel = new BookInputModel
            {
                Title = "Updated Title",
                BasePrice = 150,
                PurchaseMode = "Internet",
                AuthorId = 1,
                SubjectId = 1
            };

            // Act
            await useCase.ExecuteAsync(1, inputModel);

            // Assert
            mockBookRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Never);
            Assert.True(notificationHandler.HasNotifications());
            Assert.Contains(notificationHandler.GetNotifications(),
                n => n.Message == "Livro não encontrado.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenAuthorNotFound()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockSubjectRepository = new Mock<ISubjectRepository>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new UpdateBookUseCase(
                mockBookRepository.Object,
                mockAuthorRepository.Object,
                mockSubjectRepository.Object,
                notificationHandler);

            var existingBook = new Book("Old Title", 1, 1, PurchaseMode.Balcao, 100);
            mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync(existingBook);
            mockAuthorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                .ReturnsAsync((Author?)null);
            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync(Mock.Of<Subject>(s => s.Id == 1 && s.Description == "Sample Subject"));

            var inputModel = new BookInputModel
            {
                Title = "Updated Title",
                BasePrice = 150,
                PurchaseMode = "Internet",
                AuthorId = 1,
                SubjectId = 1
            };

            // Act
            await useCase.ExecuteAsync(1, inputModel);

            // Assert
            mockBookRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Book>()), Times.Never);
            Assert.True(notificationHandler.HasNotifications());
            Assert.Contains(notificationHandler.GetNotifications(),
                n => n.Message == "O autor especificado não existe.");
        }

        [Fact]
        public async Task ExecuteAsync_ShouldUpdateBook_WhenValidDataProvided()
        {
            // Arrange
            var mockBookRepository = new Mock<IBookRepository>();
            var mockAuthorRepository = new Mock<IAuthorRepository>();
            var mockSubjectRepository = new Mock<ISubjectRepository>();
            var notificationHandler = new DomainNotificationHandler();

            var useCase = new UpdateBookUseCase(
                mockBookRepository.Object,
                mockAuthorRepository.Object,
                mockSubjectRepository.Object,
                notificationHandler);

            var existingBook = new Book("Old Title", 1, 1, PurchaseMode.Balcao, 100);
            mockBookRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync(existingBook);
            mockAuthorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                .ReturnsAsync(Mock.Of<Author>(a => a.Id == 1 && a.Name == "Sample Author"));
            mockSubjectRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                                 .ReturnsAsync(Mock.Of<Subject>(s => s.Id == 1 && s.Description == "Sample Subject"));

            var inputModel = new BookInputModel
            {
                Title = "Updated Title",
                BasePrice = 200,
                PurchaseMode = "Evento",
                AuthorId = 1,
                SubjectId = 1
            };

            // Act
            await useCase.ExecuteAsync(1, inputModel);

            // Assert
            mockBookRepository.Verify(repo => repo.UpdateAsync(It.Is<Book>(b =>
                b.Title == "Updated Title" &&
                b.Price == 160 && // 20% desconto aplicado
                b.AuthorId == 1 &&
                b.SubjectId == 1)), Times.Once);

            Assert.False(notificationHandler.HasNotifications());
        }
    }
}
