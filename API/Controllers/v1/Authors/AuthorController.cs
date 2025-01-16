using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1.Authors
{
    [ApiController]
    [Route("api/v1/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém todos os autores.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _repository.GetAllAsync();
            return Ok(authors);
        }

        /// <summary>
        /// Obtém um autor por ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound(new { message = "Autor não encontrado." });
            }

            return Ok(author);
        }
    }
}
