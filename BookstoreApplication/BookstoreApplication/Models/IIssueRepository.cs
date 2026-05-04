namespace BookstoreApplication.Models
{
    public interface IIssueRepository
    {
        Task Add(Issue issue);
    }
}
