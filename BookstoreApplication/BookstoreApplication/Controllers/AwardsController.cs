using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private readonly IAwardService _awardService;

        public AwardsController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Award>>> GetAwards()
        {
            var awards = await _awardService.GetAll();
            return Ok(awards);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Award>> GetAward(int id)
        {
            var award = await _awardService.GetById(id);
            return Ok(award);
        }

        [HttpPost]
        public async Task<ActionResult<Award>> PostAward(Award award)
        {
            var createdAward = await _awardService.Create(award);
            return Created(string.Empty, createdAward);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Award>> PutAward(int id, Award award)
        {
            var updatedAward = await _awardService.Update(id, award);
            return Ok(updatedAward);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            await _awardService.Delete(id);
            return NoContent();
        }
    }
}
