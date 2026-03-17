namespace BookstoreApplication.Models
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();
        Task<Author> GetById(int id);
        Task<Author> Create(Author author);
        Task<Author> Update(int id, Author author);
        Task Delete(int id);
    }
}
