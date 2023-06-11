using accomodation_service.Model;

namespace accomodation_service.Service.Core
{
    public interface IAccomodationGradeService
    {
        Task<List<AccomodationGrade>> GetAllAsync();
        Task<AccomodationGrade> GetByIdAsync(Guid id);
        Task<List<AccomodationGrade>> GetAllByGuestUsernameAsync(string username);
        Task<List<AccomodationGrade>> GetAllByAccomodationIdAsync(Guid id);
        Task<List<AccomodationGrade>> GetAllByGuestAndAccomodationAsync(string guestUsername, Guid id);
        Task CreateAsync(AccomodationGrade newAccomodationGrade);
        Task UpdateAsync(Guid id, AccomodationGrade updateAccomodationGrade);
        Task DeleteAsync(Guid id);
    }
}
