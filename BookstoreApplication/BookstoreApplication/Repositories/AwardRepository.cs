using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AwardRepository
    {
        private readonly AppDbContext _context;

        public AwardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Award>> GetAll()
        {
            return await _context.Awards.ToListAsync();
        }

        public async Task<Award?> GetById(int id)
        {
            return await _context.Awards.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Award award)
        {
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();
        }

        //public async Task Update(Award award)
        //{
        //    _context.Awards.Update(award);
        //    await _context.SaveChangesAsync();
        //}

        public async Task Update(Award award)
        {
            var existing = await _context.Awards.FirstOrDefaultAsync(a => a.Id == award.Id);
            if (existing == null) return;

            existing.Name = award.Name;
            existing.Description = award.Description;
            existing.StartedYear = award.StartedYear;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var award = await GetById(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                await _context.SaveChangesAsync();
            }
        }
    }
}
