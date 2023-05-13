using AutoMapper;
using reservation_service.Model;
using search_service;

namespace reservation_service.Profiles
{
    public class ReservationProfiles : Profile
    {
        public ReservationProfiles() {
            CreateMap<GrpcSearchModel, Accomodation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
