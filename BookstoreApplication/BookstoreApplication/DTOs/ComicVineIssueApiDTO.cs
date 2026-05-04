using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class ComicVineIssueApiDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("issue_number")]
        public string? IssueNumber { get; set; }

        [JsonPropertyName("cover_date")]
        public string? CoverDate { get; set; }

        public string? Description { get; set; }
        public ComicVineImageDTO? Image { get; set; }
    }
}
