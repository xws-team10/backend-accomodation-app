using accomodation_service.Model;
using accomodation_service.Repository.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace accomodation_service.Repository
{
    public class AccomodationGradeRepository : IAccomodationGradeRepository
    {
        private readonly IMongoCollection<AccomodationGrade> _accomodationGradeCollection;

        public AccomodationGradeRepository(IOptions<AccomodationDatabaseSettings> accomodationDatabaseSettings)
        {
            var mongoClient = new MongoClient(accomodationDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(accomodationDatabaseSettings.Value.DatabaseName);
            _accomodationGradeCollection = mongoDatabase.GetCollection<AccomodationGrade>(accomodationDatabaseSettings.Value.AccomodationGradesCollectionName);
        }
        public async Task<List<AccomodationGrade>> GetAllAsync() =>
            await _accomodationGradeCollection.Find(_ => true).ToListAsync();

        public async Task<AccomodationGrade> GetGradeByIdAsync(Guid id) =>
            await _accomodationGradeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<AccomodationGrade>> GetAllByGuestUsernameAsync(string username) =>
            await _accomodationGradeCollection.Find(x => x.GuestUsername.Equals(username)).ToListAsync();

        public async Task<List<AccomodationGrade>> GetAllByAccomodationIdAsync(Guid id) =>
            await _accomodationGradeCollection.Find(x => x.AccomodationId == id).ToListAsync();

        public async Task<List<AccomodationGrade>> GetAllByGuestAndAccomodationAsync(string username, Guid id) =>
            await _accomodationGradeCollection.Find(x => x.GuestUsername.Equals(username) && x.AccomodationId.Equals(id)).ToListAsync();

        public async Task CreateAsync(AccomodationGrade newReservation) =>
            await _accomodationGradeCollection.InsertOneAsync(newReservation);

        public async Task UpdateAsync(Guid id, AccomodationGrade updateReservation) =>
            await _accomodationGradeCollection.ReplaceOneAsync(x => x.Id == id, updateReservation);

        public async Task DeleteAsync(Guid id) =>
            await _accomodationGradeCollection.DeleteOneAsync(x => x.Id == id);
    }
}
