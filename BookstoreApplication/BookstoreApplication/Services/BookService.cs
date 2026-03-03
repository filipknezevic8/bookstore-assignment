using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(AppDbContext context)
        {
            _bookRepository = new BookRepository(context);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetById(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with id {id} was not found.");
            }

            return book;
        }

        public async Task<Book> Create(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            await _bookRepository.Add(book);
            return book;
        }

        public async Task<Book> Update(int id, Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (id != book.Id)
            {
                throw new ArgumentException("Id mismatch between route and body.");
            }

            var existingBook = await _bookRepository.GetById(id);

            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with id {id} was not found.");
            }

            await _bookRepository.Update(book);
            return book;
        }

        public async Task Delete(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with id {id} was not found.");
            }

            await _bookRepository.Delete(id);
        }
    }
}
