using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublisherRepository
    {
        private readonly AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAll()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetById(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
        }

        //public async Task Update(Publisher publisher)
        //{
        //    _context.Publishers.Update(publisher);
        //    await _context.SaveChangesAsync();
        //}

        public async Task Update(Publisher publisher)
        {
            var existing = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == publisher.Id);
            if (existing == null) return;

            existing.Name = publisher.Name;
            existing.Address = publisher.Address;
            existing.Website = publisher.Website;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var publisher = await GetById(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
