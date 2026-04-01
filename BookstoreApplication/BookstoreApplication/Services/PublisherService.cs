using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Utils;

namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
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
                throw new NotFoundException(id);
            }

            return publisher;
        }

        public async Task<Publisher> Create(Publisher publisher)
        {
            if (publisher == null)
            {
                throw new BadRequestException("Publisher cannot be null.");
            }

            await _publisherRepository.Add(publisher);
            return publisher;
        }

        public async Task<Publisher> Update(int id, Publisher publisher)
        {
            if (publisher == null)
            {
                throw new BadRequestException("Publisher cannot be null.");
            }

            if (id != publisher.Id)
            {
                throw new BadRequestException("Id mismatch between route and body.");
            }

            var existingPublisher = await _publisherRepository.GetById(id);

            if (existingPublisher == null)
            {
                throw new NotFoundException(id);
            }

            await _publisherRepository.Update(publisher);
            return publisher;
        }

        public async Task Delete(int id)
        {
            var publisher = await _publisherRepository.GetById(id);

            if (publisher == null)
            {
                throw new NotFoundException(id);
            }

            await _publisherRepository.Delete(id);
        }

        public async Task<List<Publisher>> GetAllSorted(int sortType)
        {
            return await _publisherRepository.GetAllSorted(sortType);
        }

        public async Task<List<SortTypeOption>> GetSortTypes()
        {
            return await _publisherRepository.GetSortTypes();
        }
    }
}
