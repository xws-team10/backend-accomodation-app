using account_service.Model;
using account_service.Repository;

namespace account_service.Service
{
    public class HostGradeService : IHostGradeService
    {
        private readonly HostGradeRepository _repository;
        public HostGradeService(HostGradeRepository repository)
        {
            _repository = repository;   
        }

        public async Task CreateAsync(HostGrade newHostGrade) =>
            await _repository.CreateAsync(newHostGrade);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task<List<HostGrade>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<List<HostGrade>> GetAllByGuestAndHostAsync(string guestUsername, Guid hostId) =>
            await _repository.GetAllByGuestAndHostAsync(guestUsername, hostId);

        public async Task<List<HostGrade>> GetAllByGuestUsernameAsync(string guestUsername) =>
            await _repository.GetAllByGuestUsernameAsync(guestUsername);

        public async Task<List<HostGrade>> GetAllByHostIdAsync(Guid hostId) =>
            await _repository.GetAllByHostIdAsync(hostId);

        public async Task<HostGrade> GetGradeByIdAsync(Guid id) =>
            await _repository.GetGradeByIdAsync(id);

        public async Task UpdateAsync(Guid id, HostGrade updateHostGrade) =>
            await _repository.UpdateAsync(id, updateHostGrade);
    }
}
