using accomodation_service.Model;

namespace accomodation_service.Repository.Core
{
    public interface IAccomodationGradeRepository
    {
        Task<List<AccomodationGrade>> GetAllAsync();
        Task<AccomodationGrade> GetGradeByIdAsync(Guid id);
        Task<List<AccomodationGrade>> GetAllByGuestUsernameAsync(string guestUsername);
        Task<List<AccomodationGrade>> GetAllByAccomodationIdAsync(Guid id);
        Task<List<AccomodationGrade>> GetAllByGuestAndAccomodationAsync(string guestUsername, Guid id);
        Task CreateAsync(AccomodationGrade newAccomodationGrade);
        Task UpdateAsync(Guid id, AccomodationGrade updateAccomodationGrade);
        Task DeleteAsync(Guid id);
    }
}
