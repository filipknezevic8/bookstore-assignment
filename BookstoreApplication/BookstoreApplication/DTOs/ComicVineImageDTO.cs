using System.Text.Json.Serialization;

namespace BookstoreApplication.DTOs
{
    public class ComicVineImageDTO
    {
        [JsonPropertyName("small_url")]
        public string? SmallUrl { get; set; }

        [JsonPropertyName("super_url")]
        public string? SuperUrl { get; set; }
    }
}
