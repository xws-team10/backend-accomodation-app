using AutoMapper;
using reservation_service;
using search_service.DTO;

namespace search_service.Profiles
{
    public class AccomodationProfile : Profile
    {
        public AccomodationProfile() {
            CreateMap<AccomodationModel, AccomodationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
