using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Notifications;
using Domain.Repositories;

namespace Application.UseCases.Books
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly DomainNotificationHandler _notificationHandler;

        public UpdateBookUseCase(
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

        public async Task ExecuteAsync(int id, BookInputModel inputModel)
        {
            // Verifica se o livro existe
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                _notificationHandler.AddNotification(new DomainNotification("Livro", "Livro não encontrado."));
                return;
            }

            // Verifica se o autor existe
            var author = await _authorRepository.GetByIdAsync(inputModel.AuthorId);
            if (author == null)
            {
                _notificationHandler.AddNotification(new DomainNotification("Autor", "O autor especificado não existe."));
                return;
            }

            // Verifica se o assunto existe
            var subject = await _subjectRepository.GetByIdAsync(inputModel.SubjectId);
            if (subject == null)
            {
                _notificationHandler.AddNotification(new DomainNotification("Assunto", "O assunto especificado não existe."));
                return;
            }

            // Valida o modo de compra
            if (!Enum.TryParse(inputModel.PurchaseMode, true, out PurchaseMode purchaseMode))
            {
                _notificationHandler.AddNotification(new DomainNotification("Livro", "Modo de compra inválido."));
                return;
            }

            // Atualiza os detalhes do livro e ajusta o preço com base no modo de compra
            book.UpdateDetails(inputModel.Title, inputModel.BasePrice, inputModel.AuthorId, inputModel.SubjectId, purchaseMode);

            // Valida o estado atualizado do livro
            if (!book.Validate())
            {
                foreach (var error in book.ValidationResult.Errors)
                {
                    _notificationHandler.AddNotification(new DomainNotification("Livro", error.ErrorMessage));
                }
                return;
            }

            // Atualiza o livro no repositório
            await _repository.UpdateAsync(book);
        }
    }
}
