using BookstoreApplication.Utils;

namespace BookstoreApplication.Models
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task Add(Book book);
        Task Update(Book book);
        Task Delete(int id);
        Task<IEnumerable<Book>> GetAllSorted(int sortType);
        Task<List<BookSortTypeOption>> GetSortTypes();
    }
}
