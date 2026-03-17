namespace BookstoreApplication.Models
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAll();
        Task<Publisher?> GetById(int id);
        Task Add(Publisher publisher);
        Task Update(Publisher publisher);
        Task Delete(int id);
    }
}
