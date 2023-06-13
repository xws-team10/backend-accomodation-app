using accomodation_service.Repository;
using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using search_service;

namespace accomodation_service.ProtoServices
{
    public class GetAccomodationByHostService
    {
        private readonly AccomodationRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetAccomodationByHostService(AccomodationRepository repository, IConfiguration configuration, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AccomodationsResponse> GetAccommodationsByHostId(HostIdRequest request, ServerCallContext context)
        {
            string hostIdString = request.HostId;
            Guid hostId = Guid.Parse(hostIdString);

            List<Accomodation> accomodations = await _repository.GetAccomodationsByHostId(hostId);


            AccomodationsResponse response = new AccomodationsResponse();
            response.Accomodation.AddRange(accomodations);

            return response;
        }

    }
}
