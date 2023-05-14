using accomodation_service.Repository;
using accomodation_service.Service;
using AutoMapper;
using Grpc.Core;
using reservation_service;

namespace accomodation_service.ProtoServices
{
    public class GrpcCheckAccomodationsService : GrpcCheckAccomodations.GrpcCheckAccomodationsBase
    {
        private readonly AccomodationService _accomodationService;
        private readonly IMapper _mapper;

        public GrpcCheckAccomodationsService(AccomodationService accomodationRepository, IMapper mapper)
        {
            _accomodationService = accomodationRepository;
            _mapper = mapper;
        }

        public override async Task<CheckAccomodationsResponse> CheckAccomodations(CheckAccomodationsRequest request, ServerCallContext context)
        {
            var response = new CheckAccomodationsResponse();

            var isFree = await _accomodationService.AvailabilityCheck(Guid.Parse(request.Id), DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate));

            response.IsFree = isFree;
  
            return await Task.FromResult(response);
        }
    }
}
