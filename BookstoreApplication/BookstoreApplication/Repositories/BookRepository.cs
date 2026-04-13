using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        private const int PageSize = 4;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Add(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        //public void Update(Book book)
        //{
        //    _context.Books.Update(book);
        //    _context.SaveChanges();
        //}

        public async Task Update(Book book)
        {
            var existing = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            if (existing == null) return;

            existing.Title = book.Title;
            existing.PageCount = book.PageCount;
            existing.PublishedDate = book.PublishedDate;
            existing.ISBN = book.ISBN;
            existing.AuthorId = book.AuthorId;
            existing.PublisherId = book.PublisherId;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var book = await GetById(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllSorted(int sortType)
        {
            IQueryable<Book> books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            books = SortBooks(books, sortType);
            return await books.ToListAsync();
        }

        public async Task<List<BookSortTypeOption>> GetSortTypes()
        {
            List<BookSortTypeOption> options = new List<BookSortTypeOption>();
            var enumValues = Enum.GetValues(typeof(BookSortType));

            foreach (BookSortType sortType in enumValues)
            {
                options.Add(new BookSortTypeOption(sortType));
            }

            return options;
        }

        private static IQueryable<Book> SortBooks(IQueryable<Book> books, int sortType)
        {
            return sortType switch
            {
                (int)BookSortType.TITLE_DESCENDING => books.OrderByDescending(b => b.Title),
                (int)BookSortType.PUBLISHED_DATE_ASCENDING => books.OrderBy(b => b.PublishedDate),
                (int)BookSortType.PUBLISHED_DATE_DESCENDING => books.OrderByDescending(b => b.PublishedDate),
                (int)BookSortType.AUTHOR_NAME_ASCENDING => books.OrderBy(b => b.Author.FullName),
                (int)BookSortType.AUTHOR_NAME_DESCENDING => books.OrderByDescending(b => b.Author.FullName),
                _ => books.OrderBy(b => b.Title)
            };
        }

        public async Task<IEnumerable<Book>> GetAllFilteredAndSorted(BookFilter filter, int sortType)
        {
            IQueryable<Book> books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            books = FilterBooks(books, filter);
            books = SortBooks(books, sortType);

            return await books.ToListAsync();
        }

        private static IQueryable<Book> FilterBooks(IQueryable<Book> books, BookFilter filter)
        {
            if (filter == null)
            {
                return books;
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                books = books.Where(b => b.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.PublishedDateFrom != null)
            {
                books = books.Where(b => b.PublishedDate >= filter.PublishedDateFrom);
            }

            if (filter.PublishedDateTo != null)
            {
                books = books.Where(b => b.PublishedDate <= filter.PublishedDateTo);
            }

            if (!string.IsNullOrEmpty(filter.AuthorFullName))
            {
                books = books.Where(b => b.Author.FullName.ToLower().Contains(filter.AuthorFullName.ToLower()));
            }

            if (filter.AuthorId != null)
            {
                books = books.Where(b => b.AuthorId == filter.AuthorId);
            }

            if (filter.AuthorDateOfBirthFrom != null)
            {
                books = books.Where(b => b.Author.DateOfBirth >= filter.AuthorDateOfBirthFrom);
            }

            if (filter.AuthorDateOfBirthTo != null)
            {
                books = books.Where(b => b.Author.DateOfBirth <= filter.AuthorDateOfBirthTo);
            }

            return books;
        }
    }
}
