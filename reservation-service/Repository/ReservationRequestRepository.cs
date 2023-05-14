using Microsoft.Extensions.Options;
using MongoDB.Driver;
using reservation_service.Model;
using reservation_service.Repository.Core;

namespace reservation_service.Repository
{
    public class ReservationRequestRepository : IReservationRequestRepository
    {
        private readonly IMongoCollection<ReservationRequest> _reservationRequestsCollection;

        public ReservationRequestRepository(IOptions<ReservationsDatabaseSettings> reservationsDatabaseSettings)
        {
            var mongoClient = new MongoClient(reservationsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(reservationsDatabaseSettings.Value.DatabaseName);
            _reservationRequestsCollection = mongoDatabase.GetCollection<ReservationRequest>(reservationsDatabaseSettings.Value.ReservationRequestsCollectionName);
        }
        public async Task<List<ReservationRequest>> GetAllAsync() =>
            await _reservationRequestsCollection.Find(_ => true).ToListAsync();

        public async Task<ReservationRequest> GetByIdAsync(Guid id) =>
            await _reservationRequestsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<ReservationRequest>> GetAllByGuestIdAsync(Guid id) =>
            await _reservationRequestsCollection.Find(x => x.GuestId == id).ToListAsync();

        public async Task<List<ReservationRequest>> GetAllByAccomodationIdAsync(Guid id) =>
            await _reservationRequestsCollection.Find(x => x.AccomodationId == id).ToListAsync();

        public async Task CreateAsync(ReservationRequest newReservation) =>
            await _reservationRequestsCollection.InsertOneAsync(newReservation);

        public async Task UpdateAsync(Guid id, ReservationRequest updateReservation) =>
            await _reservationRequestsCollection.ReplaceOneAsync(x => x.Id == id, updateReservation);

        public async Task DeleteAsync(Guid id) =>
            await _reservationRequestsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
