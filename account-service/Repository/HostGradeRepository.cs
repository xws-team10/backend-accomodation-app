using account_service.Model;
using account_service.Repository.Core;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace account_service.Repository
{
    public class HostGradeRepository : IHostGradeRepository
    {
        private readonly IMongoCollection<HostGrade> _hostGradeCollection;

        public HostGradeRepository(IOptions<AccountDatabaseSettings> accountDatabaseSettings)
        {
            var mongoClient = new MongoClient(accountDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(accountDatabaseSettings.Value.DatabaseName);
            _hostGradeCollection = mongoDatabase.GetCollection<HostGrade>(accountDatabaseSettings.Value.HostGradesCollectionName);
        }

        public async Task CreateAsync(HostGrade newHostGrade) =>
            await _hostGradeCollection.InsertOneAsync(newHostGrade);

        public async Task DeleteAsync(Guid id) =>
            await _hostGradeCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<HostGrade>> GetAllAsync() =>        
            await _hostGradeCollection.Find(_ => true).ToListAsync();

        public async Task<List<HostGrade>> GetAllByGuestAndHostAsync(string guestUsername, Guid hostId) =>
            await _hostGradeCollection.Find(x => x.GuestUsername.Equals(guestUsername) && x.HostId.Equals(hostId)).ToListAsync();
        
        public async Task<List<HostGrade>> GetAllByGuestUsernameAsync(string guestUsername) =>
            await _hostGradeCollection.Find(x => x.GuestUsername.Equals(guestUsername)).ToListAsync();

        public async Task<List<HostGrade>> GetAllByHostIdAsync(Guid hostId) =>
            await _hostGradeCollection.Find(x => x.HostId == hostId).ToListAsync();

        public async Task<HostGrade> GetGradeByIdAsync(Guid id) =>
            await _hostGradeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        

        public async Task UpdateAsync(Guid id, HostGrade updateHostGrade) =>  
            await _hostGradeCollection.ReplaceOneAsync(x => x.Id == id, updateHostGrade);
    }
}
