using AutoMapper;
using Grpc.Core;
using reservation_service.Model;
using reservation_service.Repository;

namespace reservation_service.ProtoServices
{
    public class GrpcCheckService : GrpcCheckReservations.GrpcCheckReservationsBase
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GrpcCheckService(ReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public override async Task<CheckReservationsResponse> CheckReservations(CheckReservationsRequest request, ServerCallContext context)
        {
            var response = new CheckReservationsResponse();

            List<Reservation> reservations = await _reservationRepository.GetAllAsync();
            List<Reservation> filteredReservations = reservations.FindAll(r => r.AccomodationId.ToString().Equals(request.Id));

            foreach (Reservation reservation in filteredReservations)
            {
                if (reservation.Overlaps(DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate))){
                    response.IsFree = false;
                    return await Task.FromResult(response);
                }
                    
            }
            response.IsFree = true;
            return await Task.FromResult(response);
        }
    }
}
