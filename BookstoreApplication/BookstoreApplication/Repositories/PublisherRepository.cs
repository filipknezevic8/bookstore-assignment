using BookstoreApplication.Models;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublisherRepository : IPublisherRepository
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

        public async Task<List<Publisher>> GetAllSorted(int sortType)
        {
            IQueryable<Publisher> publishers = _context.Publishers;

            publishers = SortPublishers(publishers, sortType);
            return await publishers.ToListAsync();
        }

        private static IQueryable<Publisher> SortPublishers(IQueryable<Publisher> publishers, int sortType)
        {
            return sortType switch
            {
                (int)PublisherSortType.NAME_ASCENDING => publishers.OrderBy(p => p.Name),
                (int)PublisherSortType.NAME_DESCENDING => publishers.OrderByDescending(p => p.Name),
                (int)PublisherSortType.ADDRESS_ASCENDING => publishers.OrderBy(p => p.Address),
                (int)PublisherSortType.ADDRESS_DESCENDING => publishers.OrderByDescending(p => p.Address),
                _ => publishers.OrderBy(p => p.Name)
            };
        }

        public async Task<List<SortTypeOption>> GetSortTypes()
        {
            List<SortTypeOption> options = new List<SortTypeOption>();
            var enumValues = Enum.GetValues(typeof(PublisherSortType));

            foreach (PublisherSortType sortType in enumValues)
            {
                options.Add(new SortTypeOption(sortType));
            }

            return options;
        }
    }
}
