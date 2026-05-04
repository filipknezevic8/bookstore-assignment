using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class IssueService : IIssueService
    {
        private readonly IComicVineConnection _comicVineConnection;
        private readonly IConfiguration _config;
        private readonly IIssueRepository _issueRepository;
        private readonly IMapper _mapper;

        public IssueService(
            IComicVineConnection comicVineConnection,
            IConfiguration configuration,
            IIssueRepository issueRepository,
            IMapper mapper)
        {
            _comicVineConnection = comicVineConnection;
            _config = configuration;
            _issueRepository = issueRepository;
            _mapper = mapper;
        }

        public async Task<List<IssueDTO>> SearchIssuesByVolumeId(int volumeId)
        {
            var url = $"{_config["ComicVine:ComicVineBaseUrl"]}/issues" +
                      $"?api_key={_config["ComicVine:ComicVineAPIKey"]}" +
                      $"&format=json" +
                      $"&filter=volume:{volumeId}";

            var json = await _comicVineConnection.Get(url);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var apiItems = JsonSerializer.Deserialize<List<ComicVineIssueApiDTO>>(json, options);

            List<IssueDTO> result = new List<IssueDTO>();

            if (apiItems == null)
            {
                return result;
            }

            foreach (var item in apiItems)
            {
                result.Add(new IssueDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    IssueNumber = item.IssueNumber,
                    CoverDate = item.CoverDate,
                    Description = item.Description,
                    ImageUrl = item.Image != null ? item.Image.SmallUrl : null
                });
            }

            return result;
        }

        public async Task<Issue> Create(SaveIssueDTO data)
        {
            var issue = _mapper.Map<Issue>(data);
            issue.CreatedAt = DateTime.UtcNow;

            await _issueRepository.Add(issue);
            return issue;
        }
    }
}
