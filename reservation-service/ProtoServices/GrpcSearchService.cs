using AutoMapper;
using Grpc.Core;
using reservation_service.Repository;
using reservation_service.Repository.Core;

namespace reservation_service.ProtoServices
{
    public class GrpcSearchService : GrpcSearch.GrpcSearchBase
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GrpcSearchService(ReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public override async Task<AccomodationResponse> GetAllFreeAccomodations(GetAllRequest request, ServerCallContext context)
        {
            var response = new AccomodationResponse();
            var platforms = await _reservationRepository.GetAllAsync();
            DateTime startDate = DateTime.Parse(request.StartDate);
            DateTime endDate = DateTime.Parse(request.EndDate);
            foreach (var plat in platforms)
            {
                if ((startDate <= plat.EndDate) && (endDate >= plat.StartDate))
                    response.Accomodation.Add(_mapper.Map<AccomodationModel>(plat));
            }

            return await Task.FromResult(response);
        }

         
    }
}
