using account_service.Model;

namespace account_service.Repository.Core
{
    public interface IHostGradeRepository
    {
        Task<List<HostGrade>> GetAllAsync();
        Task<HostGrade> GetGradeByIdAsync(Guid id);
        Task<List<HostGrade>> GetAllByGuestUsernameAsync(string guestUsername);
        Task<List<HostGrade>> GetAllByHostIdAsync(Guid hostId);
        Task<List<HostGrade>> GetAllByGuestAndHostAsync(string guestUsername, Guid hostId);
        Task CreateAsync(HostGrade newHostGrade);
        Task UpdateAsync(Guid id, HostGrade updateHostGrade);
        Task DeleteAsync(Guid id);
    }
}
