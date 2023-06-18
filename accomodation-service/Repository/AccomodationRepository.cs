using accomodation_service.Repository.Core;
using accomodation_service.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using accomodation_service.Dtos;
using AutoMapper;
using host_service;



namespace accomodation_service.Repository
{
    public class AccomodationRepository : IAccomodationRepository
    {
        private readonly IMongoCollection<Model.Accomodation> _accomodationsCollection;
        private readonly IMapper _mapper;

        public AccomodationRepository(IOptions<AccomodationDatabaseSettings> accomodationDatabaseSettings, IMapper mapper)
        {
            var mongoClient = new MongoClient(accomodationDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(accomodationDatabaseSettings.Value.DatabaseName);
            _accomodationsCollection = mongoDatabase.GetCollection<Model.Accomodation>(accomodationDatabaseSettings.Value.AccomodationsCollectionName);
            _mapper = mapper;
        }

        public async Task<IEnumerable<Model.Accomodation>> GetAllAsync() =>
            await _accomodationsCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Model.Accomodation newAccomodation){
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
        public async Task<Model.Accomodation> GetAccomodationById(Guid id){
            return await _accomodationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            
        }

        public async Task<List<AccomodationModel1>> GetAccomodationsByHostId(Guid id)
        {
            List<Model.Accomodation> accomodations = await _accomodationsCollection.Find(x => x.HostId == id).ToListAsync();

            List<AccomodationModel1> mappedAccomodations = _mapper.Map<List<AccomodationModel1>>(accomodations);

            return mappedAccomodations;
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
                    return true;
                }

                return false;
            }

            return false;
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

        public async Task DeleteAccomodation(Guid id)
        {
            var result = await _accomodationsCollection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("Accomodation not found!");
            }
        }
    }
}