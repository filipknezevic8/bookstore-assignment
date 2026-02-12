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

        public List<Award> GetAll()
        {
            return _context.Awards.ToList();
        }

        public Award? GetById(int id)
        {
            return _context.Awards.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Award award)
        {
            _context.Awards.Add(award);
            _context.SaveChanges();
        }

        public void Update(Award award)
        {
            _context.Awards.Update(award);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var award = GetById(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                _context.SaveChanges();
            }
        }
    }
}
