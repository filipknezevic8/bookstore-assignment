using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet("search")]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> SearchIssuesByVolumeId([FromQuery] int volumeId)
        {
            return Ok(await _issueService.SearchIssuesByVolumeId(volumeId));
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> CreateIssue([FromBody] SaveIssueDTO data)
        {
            var createdIssue = await _issueService.Create(data);
            return Created(string.Empty, createdIssue);
        }
    }
}