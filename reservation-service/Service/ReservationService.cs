using reservation_service.Model;
using reservation_service.Repository;
using reservation_service.Service.Core;
using reservation_service;
using reservation_service.ProtoServices;

namespace reservation_service.Service
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationRepository _repository;
        private readonly ReservationRequestRepository _repositoryRequest;

        private readonly GetAccomodationByHostServiceClient _client;


        public ReservationService(ReservationRepository repository, GetAccomodationByHostServiceClient getAccomodation)
        {
            _repository = repository;
            _client = getAccomodation;
        }

        public async Task<List<Reservation>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Reservation> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<List<Reservation>> GetAllByGuestUsernameAsync(string username) =>
            await _repository.GetAllByGuestUsernameAsync(username);

        public async Task<List<Reservation>> GetAllByAccomodationIdAsync(Guid id) =>
            await _repository.GetAllByAccomodationIdAsync(id);

        public async Task CreateAsync(Reservation newReservation)
        {
            List<Reservation> reservations = await GetAllAsync();
            List<Reservation> filteredReservations = reservations.FindAll(r => r.AccomodationId.Equals(newReservation.AccomodationId));

            foreach (Reservation reservation in filteredReservations)
            {
                if (reservation.Overlaps(newReservation))
                    return;
            }

            await _repository.CreateAsync(newReservation);
        }

        public async Task UpdateAsync(Guid id, Reservation updateReservation) =>
            await _repository.UpdateAsync(id, updateReservation);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task<bool> CanGuestGradeAccomodation(String username, Guid accomodationId)
        {
            List<Reservation> guestReservations = await GetAllByGuestUsernameAsync(username);

            foreach (Reservation reservation in guestReservations)
            {
                if (reservation.AccomodationId == accomodationId && reservation.EndDate < DateTime.Now)
                    return true;
            }
            return false;
        }

        public async Task<int> GetReservationCountByHostIdAsync(string hostId)
        {
            AccomodationsResponse response = _client.GetAccommodationsByHostId(hostId);

            List<Accomodation> accommodations = response.Accomodation.ToList();

            List<Guid> accommodationIds = accommodations.Select(a => Guid.Parse(a.Id)).ToList();

            List<Reservation> reservations = await _repository.GetAllAsync();

            int reservationCount = reservations.Count(r => accommodationIds.Contains(r.AccomodationId));

            return reservationCount;
        }

        public async Task<int> GetTotalReservedDaysByHostId(string hostId)
        {
            AccomodationsResponse response = _client.GetAccommodationsByHostId(hostId);
            List<Accomodation> accommodations = response.Accomodation.ToList();

            List<Guid> accommodationIds = accommodations.Select(a => Guid.Parse(a.Id)).ToList();

            List<Reservation> reservations = await _repository.GetAllAsync();

            int totalReservedDays = 0;

            foreach (Reservation reservation in reservations)
            {
                if (accommodationIds.Contains(reservation.AccomodationId))
                {
                    TimeSpan reservationDuration = reservation.EndDate - reservation.StartDate;
                    int reservedDays = (int)reservationDuration.TotalDays;
                    totalReservedDays += reservedDays;
                }
            }

            return totalReservedDays;
        }

        public async Task<double> GetCancellationRateByHostAsync(string hostId)
        {
            AccomodationsResponse response = _client.GetAccommodationsByHostId(hostId);
            List<Accomodation> accommodations = response.Accomodation.ToList();

            List<Guid> accommodationIds = accommodations.Select(a => Guid.Parse(a.Id)).ToList();

            List<ReservationRequest> reservations = await _repositoryRequest.GetAllAsync();

            int totalReservations = 0;
            int canceledReservations = 0;

            foreach (ReservationRequest reservation in reservations)
            {
                if (accommodationIds.Contains(reservation.AccomodationId))
                {
                    totalReservations++;

                    if (reservation.Status == Status.REJECTED)
                    {
                        canceledReservations++;
                    }
                }
            }

            double cancellationRate = 0;

            if (totalReservations > 0)
            {
                cancellationRate = (double)canceledReservations / totalReservations * 100;
            }

            return cancellationRate;
        }



    }
}