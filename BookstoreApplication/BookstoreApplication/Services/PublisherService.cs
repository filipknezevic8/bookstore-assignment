using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class PublisherService
    {
        private readonly PublisherRepository _publisherRepository;

        public PublisherService(AppDbContext context)
        {
            _publisherRepository = new PublisherRepository(context);
        }

        public async Task<List<Publisher>> GetAll()
        {
            return await _publisherRepository.GetAll();
        }

        public async Task<Publisher> GetById(int id)
        {
            var publisher = await _publisherRepository.GetById(id);

            if (publisher == null)
            {
                throw new KeyNotFoundException($"Publisher with id {id} was not found.");
            }

            return publisher;
        }

        public async Task<Publisher> Create(Publisher publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException(nameof(publisher));
            }

            await _publisherRepository.Add(publisher);
            return publisher;
        }

        public async Task<Publisher> Update(int id, Publisher publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException(nameof(publisher));
            }

            if (id != publisher.Id)
            {
                throw new ArgumentException("Id mismatch between route and body.");
            }

            var existingPublisher = await _publisherRepository.GetById(id);

            if (existingPublisher == null)
            {
                throw new KeyNotFoundException($"Publisher with id {id} was not found.");
            }

            await _publisherRepository.Update(publisher);
            return publisher;
        }

        public async Task Delete(int id)
        {
            var publisher = await _publisherRepository.GetById(id);

            if (publisher == null)
            {
                throw new KeyNotFoundException($"Publisher with id {id} was not found.");
            }

            await _publisherRepository.Delete(id);
        }
    }
}
