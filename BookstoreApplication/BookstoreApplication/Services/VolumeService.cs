using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IConfiguration _config;

        public VolumeService(IComicVineConnection comicVineConnection, IConfiguration configuration)
        {
            _comicVineConnection = comicVineConnection;
            _config = configuration;
        }

        public async Task<List<VolumeDTO>> SearchVolumesByName(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new List<VolumeDTO>();
            }

            if (filter.StartsWith("name:", StringComparison.OrdinalIgnoreCase))
            {
                filter = filter.Substring("name:".Length);
            }

            var url = $"{_config["ComicVine:ComicVineBaseUrl"]}/volumes" +
                      $"?api_key={_config["ComicVine:ComicVineAPIKey"]}" +
                      $"&format=json" +
                      $"&filter=name:{Uri.EscapeDataString(filter)}";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiItems = JsonSerializer.Deserialize<List<ComicVineVolumeApiDTO>>(json, options);

            List<VolumeDTO> result = new List<VolumeDTO>();

            if (apiItems == null)
            {
                return result;
            }

            foreach (var item in apiItems)
            {
                result.Add(new VolumeDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    StartYear = item.StartYear,
                    CountOfIssues = item.CountOfIssues,
                    Description = item.Description,
                    ImageUrl = item.Image != null ? item.Image.SmallUrl : null
                });
            }

            return result;
        }
    }
}
