using BookstoreApplication.DTOs;

namespace BookstoreApplication.Models
{
    public interface IIssueService
    {
        Task<List<IssueDTO>> SearchIssuesByVolumeId(int volumeId);
        Task<Issue> Create(SaveIssueDTO data);
    }
}
