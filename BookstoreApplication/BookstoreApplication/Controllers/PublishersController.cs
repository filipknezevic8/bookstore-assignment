using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Publisher>>> GetPublishers()
        {
            var publishers = await _publisherService.GetAll();
            return Ok(publishers);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _publisherService.GetById(id);
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            var createdPublisher = await _publisherService.Create(publisher);
            return Created(string.Empty, createdPublisher);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Publisher>> PutPublisher(int id, Publisher publisher)
        {
            var updatedPublisher = await _publisherService.Update(id, publisher);
            return Ok(updatedPublisher);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            await _publisherService.Delete(id);
            return NoContent();
        }
    }
}
