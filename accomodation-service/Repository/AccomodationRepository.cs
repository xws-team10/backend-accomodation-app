using accomodation_service.Repository.Core;
using accomodation_service.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace accomodation_service.Repository
{
    public class AccomodationRepository : IAccomodationRepository
    {
        private readonly IMongoCollection<Accomodation> _accomodationsCollection;
        public AccomodationRepository(IOptions<AccomodationDatabaseSettings> accomodationDatabaseSettings)
        {
            var mongoClient = new MongoClient(accomodationDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(accomodationDatabaseSettings.Value.DatabaseName);
            _accomodationsCollection = mongoDatabase.GetCollection<Accomodation>(accomodationDatabaseSettings.Value.AccomodationsCollectionName);
        }

        public async Task<IEnumerable<Accomodation>> GetAllAsync() =>
            await _accomodationsCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Accomodation newAccomodation){
            if(newAccomodation == null){
                throw new ArgumentNullException(nameof(newAccomodation));
            }
            await _accomodationsCollection.InsertOneAsync(newAccomodation);
        }
        public async Task<Accomodation> GetAccomodationById(Guid id){
            return await _accomodationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            
        }
    }
}