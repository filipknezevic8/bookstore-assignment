using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorService(AppDbContext context)
        {
            _authorRepository = new AuthorRepository(context);
        }

        public async Task<List<Author>> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author> GetById(int id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with id {id} was not found.");
            }

            return author;
        }

        public async Task<Author> Create(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            await _authorRepository.Add(author);
            return author;
        }

        public async Task<Author> Update(int id, Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            if (id != author.Id)
            {
                throw new ArgumentException("Id mismatch between route and body.");
            }

            var existingAuthor = await _authorRepository.GetById(id);

            if (existingAuthor == null)
            {
                throw new KeyNotFoundException($"Author with id {id} was not found.");
            }

            await _authorRepository.Update(author);
            return author;
        }

        public async Task Delete(int id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with id {id} was not found.");
            }

            await _authorRepository.Delete(id);
        }
    }
}
