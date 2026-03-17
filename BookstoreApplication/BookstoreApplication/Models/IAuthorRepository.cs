namespace BookstoreApplication.Models
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();
        Task<Author?> GetById(int id);
        Task Add(Author author);
        Task Update(Author author);
        Task Delete(int id);
    }
}
