using BookstoreApplication.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Models
{
    public interface IBookService
    {
        //Task<List<Book>> GetAll();
        Task<List<BookDto>> GetAllDtos();
        Task<BookDetailsDto> GetById(int id);
        Task<Book> Create(Book book);
        Task<Book> Update(int id, Book book);
        Task Delete(int id);
        Task<IEnumerable<BookDto>> GetAllSorted(int sortType);
        Task<List<BookSortTypeOption>> GetSortTypes();
        Task<IEnumerable<BookDto>> GetAllFilteredAndSorted(BookFilter filter, int sortType);
    }
}
