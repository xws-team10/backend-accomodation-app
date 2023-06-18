using accomodation_service.Model;
using accomodation_service.Service;
using Grpc.Core;

namespace accomodation_service.ProtoServices
{
    public class GrpcGetAccomodationHostService : GrpcGetAccomodationHost.GrpcGetAccomodationHostBase
    {
        private readonly AccomodationService _accomodationService;
        public GrpcGetAccomodationHostService(AccomodationService accomodationService)
        {
            _accomodationService = accomodationService;
        }
        public override async Task<AccomodationHostResponse> GetAccomodationHost(AccomodationHostRequest request, ServerCallContext context)
        {
            Accomodation accomodation = await _accomodationService.GetAccomodationById(new Guid(request.Id));

            var response = new AccomodationHostResponse();

            response.AccomodationName = accomodation.Name;
            response.HostId = accomodation.HostId.ToString();

            return await Task.FromResult(response);
        }
    }
}
