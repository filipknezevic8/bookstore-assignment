namespace BookstoreApplication.Models
{
    public interface IBookService
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Create(Book book);
        Task<Book> Update(int id, Book book);
        Task Delete(int id);
    }
}
