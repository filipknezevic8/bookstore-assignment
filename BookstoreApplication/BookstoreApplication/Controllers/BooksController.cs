using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly PublisherRepository _publisherRepository;

        public BooksController(BookRepository bookRepository, AuthorRepository authorRepository, PublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_bookRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post(Book book)
        {
            var author = _authorRepository.GetById(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            var publisher = _publisherRepository.GetById(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            _bookRepository.Add(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var existing = _bookRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }

            var author = _authorRepository.GetById(book.AuthorId);
            if (author == null)
            {
                return BadRequest();
            }

            var publisher = _publisherRepository.GetById(book.PublisherId);
            if (publisher == null)
            {
                return BadRequest();
            }

            _bookRepository.Update(book);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Delete(id);
            return NoContent();
        }
    }
}
