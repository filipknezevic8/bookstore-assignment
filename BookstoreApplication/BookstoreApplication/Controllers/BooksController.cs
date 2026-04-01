using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _bookService.GetAll();
            return Ok(books);
        }

        [HttpGet("dtos")]
        public async Task<ActionResult<List<BookDto>>> GetBookDtos()
        {
            var books = await _bookService.GetAllDtos();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _bookService.GetById(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            var createdBook = await _bookService.Create(book);
            return Created(string.Empty, createdBook);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Book>> PutBook(int id, Book book)
        {
            var updatedBook = await _bookService.Update(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.Delete(id);
            return NoContent();
        }
    }
}
