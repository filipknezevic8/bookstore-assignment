namespace BookstoreApplication.Models
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetAll();
        Task<Publisher> GetById(int id);
        Task<Publisher> Create(Publisher publisher);
        Task<Publisher> Update(int id, Publisher publisher);
        Task Delete(int id);
    }
}
