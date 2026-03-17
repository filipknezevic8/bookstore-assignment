using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _awardRepository;

        public AwardService(IAwardRepository awardRepository)
        {
            _awardRepository = awardRepository;
        }

        public async Task<List<Award>> GetAll()
        {
            return await _awardRepository.GetAll();
        }

        public async Task<Award> GetById(int id)
        {
            var award = await _awardRepository.GetById(id);

            if (award == null)
            {
                throw new KeyNotFoundException($"Award with id {id} was not found.");
            }

            return award;
        }

        public async Task<Award> Create(Award award)
        {
            if (award == null)
            {
                throw new ArgumentNullException(nameof(award));
            }

            await _awardRepository.Add(award);
            return award;
        }

        public async Task<Award> Update(int id, Award award)
        {
            if (award == null)
            {
                throw new ArgumentNullException(nameof(award));
            }

            if (id != award.Id)
            {
                throw new ArgumentException("Id mismatch between route and body.");
            }

            var existingAward = await _awardRepository.GetById(id);

            if (existingAward == null)
            {
                throw new KeyNotFoundException($"Award with id {id} was not found.");
            }

            await _awardRepository.Update(award);
            return award;
        }

        public async Task Delete(int id)
        {
            var award = await _awardRepository.GetById(id);

            if (award == null)
            {
                throw new KeyNotFoundException($"Award with id {id} was not found.");
            }

            await _awardRepository.Delete(id);
        }
    }
}
