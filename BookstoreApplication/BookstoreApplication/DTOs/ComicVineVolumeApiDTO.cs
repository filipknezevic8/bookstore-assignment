using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class ComicVineVolumeApiDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("start_year")]
        public string? StartYear { get; set; }

        [JsonPropertyName("count_of_issues")]
        public int? CountOfIssues { get; set; }

        public string? Description { get; set; }
        public ComicVineImageDTO? Image { get; set; }
    }
}
