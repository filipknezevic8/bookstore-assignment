using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetById(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        //public async Task Update(Author author)
        //{
        //    _context.Authors.Update(author);
        //    await _context.SaveChangesAsync();
        //}

        public async Task Update(Author author)
        {
            var existing = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            if (existing == null) return;

            existing.FullName = author.FullName;
            existing.Biography = author.Biography;
            existing.DateOfBirth = author.DateOfBirth;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var author = await GetById(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
