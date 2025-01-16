using Application.Models;
using Application.UseCases.Books;
using Domain.Notifications;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1.Books
{
    [ApiController]
    [Route("api/v1/books")]
    public class BookController : ControllerBase
    {
        private readonly IAddBookUseCase _addBookUseCase;
        private readonly IUpdateBookUseCase _updateBookUseCase;
        private readonly IBookRepository _repository;
        private readonly DomainNotificationHandler _notificationHandler;

        public BookController(
            IAddBookUseCase addBookUseCase,
            IUpdateBookUseCase updateBookUseCase,
            IBookRepository repository,
            DomainNotificationHandler notificationHandler)
        {
            _addBookUseCase = addBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _repository = repository;
            _notificationHandler = notificationHandler;
        }

        /// <summary>
        /// Adiciona um novo livro.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBook([FromBody] BookInputModel inputModel)
        {
            await _addBookUseCase.ExecuteAsync(inputModel);

            if (_notificationHandler.HasNotifications())
            {
                var errors = _notificationHandler.GetNotifications().Select(n => n.Message);
                return BadRequest(new { Errors = errors });
            }

            return CreatedAtAction(nameof(AddBook), new { message = "Livro adicionado com sucesso." });
        }

        /// <summary>
        /// Atualiza um livro existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookInputModel inputModel)
        {
            await _updateBookUseCase.ExecuteAsync(id, inputModel);

            if (_notificationHandler.HasNotifications())
            {
                var errors = _notificationHandler.GetNotifications().Select(n => n.Message);
                return BadRequest(new { Errors = errors });
            }

            return Ok(new { message = "Livro atualizado com sucesso." });
        }

        /// <summary>
        /// Obtém todos os livros.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _repository.GetAllAsync();
            return Ok(books);
        }

        /// <summary>
        /// Obtém um livro por ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Livro não encontrado." });
            }

            return Ok(book);
        }

        /// <summary>
        /// Remove um livro por ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Livro não encontrado." });
            }

            await _repository.DeleteAsync(id);
            return Ok(new { message = "Livro removido com sucesso." });
        }
    }
}
