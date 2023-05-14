using Microsoft.Extensions.Options;
using MongoDB.Driver;
using search_service.DTO;
using search_service.Model;
using search_service.Repository.Core;

namespace search_service.Repository
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
        public async Task<List<Accomodation>> GetAllAsync() =>
            await _accomodationsCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Accomodation newAccomodation) =>
            await _accomodationsCollection.InsertOneAsync(newAccomodation);

        public async Task AccomodationUpdate(AccomodationUpdateDto accomodationChangeDto)
        {
            var accomodation = _accomodationsCollection.Find(x => x.Id == accomodationChangeDto.Id).FirstOrDefault();
            if (accomodation != null)
            {
                accomodation.AvailableToDate = accomodationChangeDto.AvailableToDate;
                accomodation.AvailableFromDate = accomodationChangeDto.AvailableFromDate;
                if (accomodation.AvailabilityInitialValidate())
                {
                    await _accomodationsCollection.ReplaceOneAsync(x => x.Id == accomodation.Id, accomodation);
                    return;
                }

                throw new Exception("Invalid date time!");
            }

            throw new ArgumentNullException(nameof(accomodationChangeDto));
        }
    }
}
