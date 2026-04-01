using BookstoreApplication.DTOs;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Models
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();
        Task<Author> GetById(int id);
        Task<Author> Create(Author author);
        Task<Author> Update(int id, Author author);
        Task Delete(int id);
        Task<PaginatedList<AuthorDTO>> GetAllPaged(int page);
    }
}
