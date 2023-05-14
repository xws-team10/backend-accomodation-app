using Microsoft.Extensions.Options;
using MongoDB.Driver;
using reservation_service.Model;
using reservation_service.Repository.Core;

namespace reservation_service.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservationsCollection;

        public ReservationRepository(IOptions<ReservationsDatabaseSettings> reservationsDatabaseSettings)
        {
            var mongoClient = new MongoClient(reservationsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(reservationsDatabaseSettings.Value.DatabaseName);
            _reservationsCollection = mongoDatabase.GetCollection<Reservation>(reservationsDatabaseSettings.Value.ReservationsCollectionName);
        }
        public async Task<List<Reservation>> GetAllAsync() =>
            await _reservationsCollection.Find(_ => true).ToListAsync();

        public async Task<Reservation> GetByIdAsync(Guid id) =>
            await _reservationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Reservation>> GetAllByGuestIdAsync(Guid id) =>
            await _reservationsCollection.Find(x => x.GuestId == id).ToListAsync();

        public async Task<List<Reservation>> GetAllByAccomodationIdAsync(Guid id) =>
            await _reservationsCollection.Find(x => x.AccomodationId == id).ToListAsync();

        public async Task CreateAsync(Reservation newReservation) =>
            await _reservationsCollection.InsertOneAsync(newReservation);

        public async Task UpdateAsync(Guid id, Reservation updateReservation) =>
            await _reservationsCollection.ReplaceOneAsync(x => x.Id == id, updateReservation);

        public async Task DeleteAsync(Guid id) =>
            await _reservationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
