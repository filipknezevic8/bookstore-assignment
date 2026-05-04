namespace BookstoreApplication.Models
{
    public interface IComicVineConnection
    {
        Task<string> Get(string url);
    }
}
