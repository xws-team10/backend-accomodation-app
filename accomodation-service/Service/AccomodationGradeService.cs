using accomodation_service.Model;
using accomodation_service.Repository;
using accomodation_service.Service.Core;

namespace accomodation_service.Service
{
    public class AccomodationGradeService : IAccomodationGradeService
    {
        private readonly AccomodationGradeRepository _repository;

        public AccomodationGradeService(AccomodationGradeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AccomodationGrade>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<AccomodationGrade> GetByIdAsync(Guid id) =>
            await _repository.GetGradeByIdAsync(id);

        public async Task<List<AccomodationGrade>> GetAllByGuestUsernameAsync(string username) =>
            await _repository.GetAllByGuestUsernameAsync(username);

        public async Task<List<AccomodationGrade>> GetAllByAccomodationIdAsync(Guid id) =>
            await _repository.GetAllByAccomodationIdAsync(id);

        public async Task<List<AccomodationGrade>> GetAllByGuestAndAccomodationAsync(string username, Guid id) =>
            await _repository.GetAllByGuestAndAccomodationAsync(username, id);
        public async Task CreateAsync(AccomodationGrade newAccomodationGrade)
        {
            await _repository.CreateAsync(newAccomodationGrade);
        }

        public async Task UpdateAsync(Guid id, AccomodationGrade updateAccomodationGrade) =>
            await _repository.UpdateAsync(id, updateAccomodationGrade);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}