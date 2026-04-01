using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetById(id);
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            var createdAuthor = await _authorService.Create(author);
            return Created(string.Empty, createdAuthor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Author>> PutAuthor(int id, Author author)
        {
            var updatedAuthor = await _authorService.Update(id, author);
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.Delete(id);
            return NoContent();
        }

        // GET /api/authors/paging?page=2
        [HttpGet("paging")]
        public async Task<ActionResult<PaginatedList<AuthorDTO>>> GetAuthorsPage([FromQuery] int page = 1)
        {
            if (page < 1)
            {
                throw new BadRequestException("Page value is invalid.");
            }

            return Ok(await _authorService.GetAllPaged(page));
        }
    }
}
