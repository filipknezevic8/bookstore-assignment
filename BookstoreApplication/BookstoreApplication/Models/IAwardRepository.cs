namespace BookstoreApplication.Models
{
    public interface IAwardRepository
    {
        Task<List<Award>> GetAll();
        Task<Award?> GetById(int id);
        Task Add(Award award);
        Task Update(Award award);
        Task Delete(int id);
    }
}
