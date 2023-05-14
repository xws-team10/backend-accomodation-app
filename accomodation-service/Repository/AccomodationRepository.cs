using accomodation_service.Repository.Core;
using accomodation_service.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using accomodation_service.Dtos;

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
            var accomodation = _accomodationsCollection.Find(x => x.Name == newAccomodation.Name).FirstOrDefault();
            if(accomodation == null)
            {
                 await _accomodationsCollection.InsertOneAsync(newAccomodation);
                 return;
            }

            throw new Exception("Accomodation with that name already exists!");
            
        }
        public async Task<Accomodation> GetAccomodationById(Guid id){
            return await _accomodationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            
        }

        public async Task AccomodationUpdate(AccomodationChangeDto accomodationChangeDto)
        {
            var accomodation = _accomodationsCollection.Find(x => x.Id == accomodationChangeDto.Id).FirstOrDefault();
            if(accomodation != null)
            {
                accomodation.AvailableToDate = accomodationChangeDto.AvailableToDate;
                accomodation.AvailableFromDate = accomodationChangeDto.AvailableFromDate;
                if(accomodation.AvailabilityInitialValidate())
                {
                    await _accomodationsCollection.ReplaceOneAsync(x => x.Id == accomodation.Id, accomodation);
                    return;
                }

                throw new Exception("Invalid date time!");
            }

            throw new ArgumentNullException(nameof(accomodationChangeDto));
        }

        public async Task AccomodationChangePrice(AccomodationChangePriceDto accomodationChangePriceDto)
        {
            var accomodation = _accomodationsCollection.Find(x => x.Id == accomodationChangePriceDto.Id).FirstOrDefault();
            if(accomodation != null)
            {
                accomodation.Price = accomodationChangePriceDto.Price;
                await _accomodationsCollection.ReplaceOneAsync(x => x.Id == accomodation.Id, accomodation);
                return;
            }

            throw new ArgumentNullException(nameof(accomodationChangePriceDto));
        }
    }
}