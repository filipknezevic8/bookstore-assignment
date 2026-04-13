using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;
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

        //[HttpGet("entity")]
        //public async Task<ActionResult<List<Book>>> GetBooks()
        //{
        //    var books = await _bookService.GetAll();
        //    return Ok(books);
        //}

        [HttpGet]
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

        [HttpGet("sortTypes")]
        public async Task<IActionResult> GetSortTypes()
        {
            return Ok(await _bookService.GetSortTypes());
        }

        [HttpGet("sort")]
        public async Task<IActionResult> GetSortedBooks([FromQuery] int sortType = (int)BookSortType.TITLE_ASCENDING)
        {
            return Ok(await _bookService.GetAllSorted(sortType));
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            var createdBook = await _bookService.Create(book);
            return Created(string.Empty, createdBook);
        }

        [HttpPost("filterAndSort")]
        public async Task<IActionResult> GetFilteredAndSortedBooks([FromBody] BookFilter filter, [FromQuery] int sortType = (int)BookSortType.TITLE_ASCENDING)
        {
            return Ok(await _bookService.GetAllFilteredAndSorted(filter, sortType));
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
