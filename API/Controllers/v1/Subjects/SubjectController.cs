using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1.Subjects
{
    [ApiController]
    [Route("api/v1/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _repository;

        public SubjectController(ISubjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtém todos os assuntos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _repository.GetAllAsync();
            return Ok(subjects);
        }

        /// <summary>
        /// Obtém um assunto por ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound(new { message = "Assunto não encontrado." });
            }

            return Ok(subject);
        }
    }
}
