using AutoMapper;
using Grpc.Core;
using search_service.Repository;
using search_service.Repository.Core;

namespace search_service.Service.Grpc
{
    public class GrpcSearchServise : GrpcSearch.GrpcSearchBase
    {
        private readonly AccomodationRepository repository;
        private readonly IMapper _mapper;

        public GrpcSearchServise(AccomodationRepository repository, IMapper mapper)
        {
            this.repository = repository;  
            this._mapper = mapper;
        }

        public override async Task<SearchResponse> GetAllAccom(GetAllRequest request, ServerCallContext context)
        {

            var response = new SearchResponse();
            var platforms = await repository.GetAllAsync();
            Guid id = Guid.Parse(request.Id);
            foreach (var plat in platforms)
            {
                if(id.Equals(plat.Id))
                    response.Search.Add(_mapper.Map<GrpcSearchModel>(plat));
            }

            return await Task.FromResult(response);
        }
    }
}
