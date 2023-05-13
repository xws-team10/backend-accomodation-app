using AutoMapper;
using search_service.Model;

namespace search_service.Profiles
{
    public class AccomodationProfiles : Profile
    {
        public AccomodationProfiles()
        {
            CreateMap<Accomodation,GrpcSearchModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
