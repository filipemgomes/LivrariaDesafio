using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Notifications;
using Domain.Repositories;

namespace Application.UseCases.Books
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly DomainNotificationHandler _notificationHandler;

        public AddBookUseCase(
            IBookRepository repository,
            IAuthorRepository authorRepository,
            ISubjectRepository subjectRepository,
            DomainNotificationHandler notificationHandler)
        {
            _repository = repository;
            _authorRepository = authorRepository;
            _subjectRepository = subjectRepository;
            _notificationHandler = notificationHandler;
        }

        public async Task ExecuteAsync(BookInputModel inputModel)
        {
            // Validação de entrada
            if (string.IsNullOrEmpty(inputModel.Title))
            {
                _notificationHandler.AddNotification(new DomainNotification("Livro", "O título do livro é obrigatório."));
                return;
            }

            if (inputModel.AuthorId <= 0 || inputModel.SubjectId <= 0)
            {
                _notificationHandler.AddNotification(new DomainNotification("Livro", "Autor e Assunto são obrigatórios."));
                return;
            }

            if (inputModel.BasePrice <= 0)
            {
                _notificationHandler.AddNotification(new DomainNotification("Livro", "O preço base do livro deve ser maior que zero."));
                return;
            }

            if (!Enum.TryParse(inputModel.PurchaseMode, true, out PurchaseMode purchaseMode))
            {
                _notificationHandler.AddNotification(new DomainNotification("Livro", "Modo de compra inválido."));
                return;
            }

            // Verificação da existência de autor e assunto
            var authorExists = await _authorRepository.GetByIdAsync(inputModel.AuthorId);
            if (authorExists == null)
            {
                _notificationHandler.AddNotification(new DomainNotification("Autor", "O autor especificado não existe."));
                return;
            }

            var subjectExists = await _subjectRepository.GetByIdAsync(inputModel.SubjectId);
            if (subjectExists == null)
            {
                _notificationHandler.AddNotification(new DomainNotification("Assunto", "O assunto especificado não existe."));
                return;
            }

            // Criação do livro e ajuste do preço com base no PurchaseMode
            var book = new Book(inputModel.Title, inputModel.AuthorId, inputModel.SubjectId, purchaseMode, inputModel.BasePrice);

            // Persistência
            await _repository.AddAsync(book);
        }
    }
}
