using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        //public void Update(Book book)
        //{
        //    _context.Books.Update(book);
        //    _context.SaveChanges();
        //}

        public void Update(Book book)
        {
            var existing = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existing == null) return;

            existing.Title = book.Title;
            existing.PageCount = book.PageCount;
            existing.PublishedDate = book.PublishedDate;
            existing.ISBN = book.ISBN;
            existing.AuthorId = book.AuthorId;
            existing.PublisherId = book.PublisherId;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
