using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublisherRepository _publisherRepository;

        public PublishersController(PublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_publisherRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var publisher = _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult Post(Publisher publisher)
        {
            _publisherRepository.Add(publisher);
            return Ok(publisher);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            var existing = _publisherRepository.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }

            _publisherRepository.Update(publisher);
            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var publisher = _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _publisherRepository.Delete(id);
            return NoContent();
        }
    }
}
