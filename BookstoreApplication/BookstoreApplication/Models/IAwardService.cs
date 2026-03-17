namespace BookstoreApplication.Models
{
    public interface IAwardService
    {
        Task<List<Award>> GetAll();
        Task<Award> GetById(int id);
        Task<Award> Create(Award award);
        Task<Award> Update(int id, Award award);
        Task Delete(int id);
    }
}
